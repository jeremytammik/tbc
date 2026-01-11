/**
 * toc-sidebar.js - Wiki-Style TOC Sidebar
 * 
 * For The Building Coder Archive
 * Author: GitHub Copilot
 * Date: January 2, 2026
 * 
 * Features:
 * - Dynamic sidebar generation from JSON data
 * - Topic-based navigation with collapsible groups
 * - Real-time search with highlighting
 * - Drag-to-resize functionality
 * - Current page detection and highlighting
 * - Mobile hamburger menu
 * - State persistence in localStorage
 */

(function() {
  'use strict';

  // ================================
  // Configuration
  // ================================
  const CONFIG = {
    tocDataUrl: 'toc/toc-data.json',
    searchIndexUrl: 'toc/search-index.json',
    enableContentSearch: true,
    searchCacheTime: 24 * 60 * 60 * 1000, // 24 hours
    defaultWidth: 280,
    minWidth: 180,
    maxWidth: 500,
    searchDebounce: 150,
    storageKeys: {
      width: 'tbc-sidebar-width',
      expanded: 'tbc-expanded-topics',
      scroll: 'tbc-sidebar-scroll',
      searchIndex: 'tbc-sidebar-search-index',
      searchIndexTime: 'tbc-sidebar-search-index-time',
      searchInContent: 'tbc-sidebar-search-in-content'
    }
  };

  // ================================
  // State
  // ================================
  const state = {
    tocData: null,
    currentPage: null,
    expandedTopics: new Set(),
    isResizing: false,
    isMobileOpen: false,
    searchQuery: '',
    searchIndex: null,
    searchIndexLoaded: false,
    searchInContent: false
  };

  // ================================
  // Match Type Constants (Content Match Indicator)
  // ================================
  const MATCH_TYPE = Object.freeze({
    NONE: 0,
    TITLE_ONLY: 1,
    CONTENT_ONLY: 2,
    BOTH: 3
  });

  /**
   * Determine the type of match for a post against a search query
   * @param {Object} post - Post object with title and contentPreview
   * @param {string} query - Lowercase search query
   * @returns {number} MATCH_TYPE value
   */
  function getMatchType(post, query) {
    const titleMatch = post.title && post.title.toLowerCase().includes(query);
    // contentPreview is stored in lowercase in the search index (see spec),
    // and `query` is already lowercase, so we can compare directly.
    const contentMatch = post.contentPreview &&
      post.contentPreview.includes(query);
    
    if (titleMatch && contentMatch) return MATCH_TYPE.BOTH;
    if (titleMatch) return MATCH_TYPE.TITLE_ONLY;
    if (contentMatch) return MATCH_TYPE.CONTENT_ONLY;
    return MATCH_TYPE.NONE;
  }

  /**
   * Generate an excerpt showing context around the matched term
   * Uses the proper-case 'excerpt' field for display when available,
   * falls back to 'contentPreview' for longer context.
   * 
   * @param {Object} postData - Post data with excerpt and contentPreview
   * @param {string} query - Search query (lowercase)
   * @param {number} maxLength - Maximum excerpt length (default 80)
   * @returns {Object|null} { text: string, query: string, isLowercase: boolean } or null
   */
  function generateExcerptForDisplay(postData, query, maxLength = 80) {
    if (!query || typeof query !== 'string' || query.length === 0) return null;
    
    // Limit query length to prevent ReDoS
    const safeQuery = query.substring(0, 100);
    
    // Prefer the proper-case excerpt field if it contains the match
    const excerpt = postData.excerpt || '';
    const contentPreview = postData.contentPreview || '';
    
    // Check which field contains the match
    const excerptHasMatch = excerpt.toLowerCase().includes(safeQuery);
    const sourceText = excerptHasMatch ? excerpt : contentPreview;
    
    if (!sourceText || sourceText.length === 0) return null;
    
    const lowerSource = sourceText.toLowerCase();
    const matchIndex = lowerSource.indexOf(safeQuery);
    
    if (matchIndex === -1) return null;
    
    // Calculate window around match
    const halfWindow = Math.floor((maxLength - safeQuery.length) / 2);
    let start = Math.max(0, matchIndex - halfWindow);
    let end = Math.min(sourceText.length, matchIndex + safeQuery.length + halfWindow);
    
    // Adjust to word boundaries (don't cut words in half)
    if (start > 0) {
      const spaceAfterStart = sourceText.indexOf(' ', start);
      if (spaceAfterStart !== -1 && spaceAfterStart < matchIndex) {
        start = spaceAfterStart + 1;
      }
    }
    if (end < sourceText.length) {
      const spaceBeforeEnd = sourceText.lastIndexOf(' ', end);
      if (spaceBeforeEnd > matchIndex + safeQuery.length) {
        end = spaceBeforeEnd;
      }
    }
    
    let excerptText = sourceText.substring(start, end).trim();
    
    // Add ellipsis indicators
    if (start > 0) excerptText = '...' + excerptText;
    if (end < sourceText.length) excerptText = excerptText + '...';
    
    return {
      text: excerptText,
      query: safeQuery,
      isLowercase: !excerptHasMatch  // Flag if using lowercase contentPreview
    };
  }

  /**
   * Create excerpt DOM element with highlighted search term
   * @param {Object} excerptData - Result from generateExcerptForDisplay()
   * @param {string} postFile - Post filename for unique ID
   * @returns {HTMLElement} Excerpt container element
   */
  function createExcerptElement(excerptData, postFile) {
    const container = document.createElement('div');
    container.className = 'tbc-excerpt collapsed';
    container.id = `excerpt-${postFile.replace(/[^a-zA-Z0-9-]/g, '-')}`;
    container.setAttribute('role', 'note');
    container.setAttribute('aria-label', 'Content excerpt');
    
    const textSpan = document.createElement('span');
    textSpan.className = 'tbc-excerpt-text';
    
    // Add lowercase indicator if using contentPreview fallback
    if (excerptData.isLowercase) {
      textSpan.classList.add('tbc-excerpt-lowercase');
    }
    
    // Highlight the search term within the excerpt using DOM nodes to avoid breaking HTML entities
    const query = excerptData.query;
    if (!query) {
      // No query to highlight; just render the excerpt text with quotes
      textSpan.textContent = `"${excerptData.text}"`;
    } else {
      const regex = new RegExp(`(${escapeRegex(query)})`, 'gi');
      const parts = excerptData.text.split(regex);
      
      // Add opening quote
      textSpan.appendChild(document.createTextNode('"'));
      
      parts.forEach((part, index) => {
        if (part === '') {
          return;
        }
        if (index % 2 === 0) {
          // Non-matching text segment
          textSpan.appendChild(document.createTextNode(part));
        } else {
          // Matching segment: wrap in <mark>
          const mark = document.createElement('mark');
          mark.textContent = part;
          textSpan.appendChild(mark);
        }
      });
      
      // Add closing quote
      textSpan.appendChild(document.createTextNode('"'));
    }
    
    const toggleBtn = document.createElement('button');
    toggleBtn.className = 'tbc-excerpt-toggle';
    toggleBtn.setAttribute('aria-label', 'Expand excerpt');
    toggleBtn.setAttribute('aria-expanded', 'false');
    toggleBtn.textContent = '▼';
    
    toggleBtn.addEventListener('click', (e) => {
      e.preventDefault();
      e.stopPropagation();
      const isExpanded = container.classList.contains('expanded');
      container.classList.toggle('expanded');
      container.classList.toggle('collapsed');
      toggleBtn.setAttribute('aria-expanded', !isExpanded);
      toggleBtn.setAttribute('aria-label', isExpanded ? 'Expand excerpt' : 'Collapse excerpt');
      toggleBtn.textContent = isExpanded ? '▼' : '▲';
    });
    
    container.appendChild(textSpan);
    container.appendChild(toggleBtn);
    
    return container;
  }

  /**
   * Remove all excerpt elements and content-match classes
   * Used when clearing search or toggling content search off
   */
  function removeAllExcerpts() {
    const excerpts = document.querySelectorAll('.tbc-excerpt');
    excerpts.forEach(excerpt => excerpt.remove());
    
    // Also remove content-match class from all posts
    const contentMatches = document.querySelectorAll('.tbc-content-match');
    contentMatches.forEach(el => el.classList.remove('tbc-content-match'));
  }

  // ================================
  // Utility Functions
  // ================================
  function debounce(func, wait) {
    let timeout;
    return function(...args) {
      clearTimeout(timeout);
      timeout = setTimeout(() => func.apply(this, args), wait);
    };
  }

  function escapeHtml(text) {
    const div = document.createElement('div');
    div.textContent = text;
    return div.innerHTML;
  }

  function escapeRegex(string) {
    return string.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
  }

  function getCurrentPageFile() {
    const path = window.location.pathname;
    const file = path.split('/').pop();
    return file || 'index.html';
  }

  // ================================
  // Data Loading
  // ================================
  async function loadTocData() {
    // Try to load from cache first
    const cached = localStorage.getItem('tbc-toc-data');
    const cacheTime = localStorage.getItem('tbc-toc-cache-time');
    const ONE_HOUR = 60 * 60 * 1000;
    
    if (cached && cacheTime && (Date.now() - parseInt(cacheTime)) < ONE_HOUR) {
      try {
        return JSON.parse(cached);
      } catch (e) {
        console.warn('Failed to parse cached TOC data');
      }
    }
    
    // Determine the base path for the JSON file
    const currentPath = window.location.pathname;
    let basePath = '';
    
    // If we're in the a/ directory viewing a post (has /a/ in path or served directly from a/)
    if (currentPath.includes('/a/') && !currentPath.endsWith('/a/') && !currentPath.endsWith('/a/index.html')) {
      basePath = '';  // toc/ is in same directory
    } else if (currentPath.endsWith('/a/') || currentPath.endsWith('/a/index.html')) {
      basePath = '';  // we're in a/
    } else if (currentPath === '/index.html' || currentPath === '/' || currentPath.match(/^\/\d{4}_/)) {
      // Serving directly from a/ directory (local dev or subdomain).
      // Post filenames are always 4-digit zero-padded (e.g. /0001_title.html),
      // so /^\/\d{4}_/ intentionally matches those post URLs at the root.
      basePath = '';
    } else {
      basePath = 'a/';  // we're at root
    }
    
    const url = basePath + CONFIG.tocDataUrl;
    
    try {
      const response = await fetch(url);
      if (!response.ok) {
        throw new Error(`HTTP ${response.status}`);
      }
      const data = await response.json();
      
      // Cache the data
      try {
        localStorage.setItem('tbc-toc-data', JSON.stringify(data));
        localStorage.setItem('tbc-toc-cache-time', Date.now().toString());
      } catch (e) {
        console.warn('Failed to cache TOC data');
      }
      
      return data;
    } catch (error) {
      console.error('Failed to load TOC data:', error);
      throw error;
    }
  }

  async function loadSearchIndex() {
    if (!CONFIG.enableContentSearch) {
      return;
    }

    // Check cache first
    try {
      const cached = localStorage.getItem(CONFIG.storageKeys.searchIndex);
      const cacheTime = localStorage.getItem(CONFIG.storageKeys.searchIndexTime);

      if (cached && cacheTime) {
        const cachedData = JSON.parse(cached);
        const cacheTimeNum = Number.parseInt(cacheTime, 10);

        if (Number.isFinite(cacheTimeNum) && cacheTimeNum >= 0) {
          const age = Date.now() - cacheTimeNum;

          // Validate cache structure
          const isValid = cachedData &&
            typeof cachedData === 'object' &&
            typeof cachedData.version === 'string' &&
            typeof cachedData.totalPosts === 'number' &&
            Array.isArray(cachedData.posts);

          // Use cache if valid and not expired
          if (isValid && age >= 0 && age < CONFIG.searchCacheTime && cachedData.version === '1.0') {
            console.log('Using cached search index');
            state.searchIndex = cachedData;
            state.searchIndexLoaded = true;
            updateContentToggleState();
            return;
          }
        } else {
          console.warn('Ignoring search index cache due to invalid cache time value:', cacheTime);
        }
      }
    } catch (e) {
      console.warn('Failed to load cached search index');
    }

    // Fetch fresh index
    const currentPath = window.location.pathname;
    let basePath = '';

    if (currentPath.includes('/a/') && !currentPath.endsWith('/a/') && !currentPath.endsWith('/a/index.html')) {
      basePath = '';
    } else if (currentPath.endsWith('/a/') || currentPath.endsWith('/a/index.html')) {
      basePath = '';
    } else if (currentPath === '/index.html' || currentPath === '/' || currentPath.match(/^\/\d{4}_/)) {
      basePath = '';
    } else {
      basePath = 'a/';
    }

    try {
      const url = basePath + CONFIG.searchIndexUrl;
      const FETCH_TIMEOUT_MS = 10000;
      const controller = new AbortController();
      const { signal } = controller;
      let timeoutId;

      try {
        timeoutId = window.setTimeout(() => {
          controller.abort();
        }, FETCH_TIMEOUT_MS);

        const response = await fetch(url, { signal });

        if (!response.ok) {
          throw new Error(`HTTP ${response.status}`);
        }

        const index = await response.json();
        state.searchIndex = index;
        state.searchIndexLoaded = true;

        // Cache it
        try {
          localStorage.setItem(CONFIG.storageKeys.searchIndex, JSON.stringify(index));
          localStorage.setItem(CONFIG.storageKeys.searchIndexTime, Date.now().toString());
        } catch (e) {
          console.warn('Failed to cache search index');
        }

        console.log(`Search index loaded: ${index.totalPosts} posts`);
        updateContentToggleState();
      } finally {
        if (timeoutId !== undefined) {
          clearTimeout(timeoutId);
        }
      }
    } catch (error) {
      if (error.name === 'AbortError') {
        console.error('Failed to load search index: request timed out');
      } else {
        console.error('Failed to load search index:', error);
      }
      state.searchIndexLoaded = false;
      updateContentToggleState();
    }
  }

  function updateContentToggleState() {
    const toggle = document.getElementById('tbc-search-content-toggle');
    if (!toggle) return;

    if (state.searchIndexLoaded) {
      toggle.disabled = false;
      toggle.title = 'Search in post content as well as titles';
      // Restore saved preference
      const savedPref = localStorage.getItem(CONFIG.storageKeys.searchInContent);
      if (savedPref === 'true') {
        toggle.checked = true;
        state.searchInContent = true;
      }
    } else {
      toggle.disabled = true;
      toggle.checked = false;
      toggle.title = 'Search index not available';
      state.searchInContent = false;
    }
  }

  // ================================
  // State Persistence
  // ================================
  function loadPersistedState() {
    // Load expanded topics
    try {
      const expanded = localStorage.getItem(CONFIG.storageKeys.expanded);
      if (expanded) {
        state.expandedTopics = new Set(JSON.parse(expanded));
      }
    } catch (e) {
      console.warn('Failed to load expanded topics');
    }
    
    // Load sidebar width
    const savedWidth = localStorage.getItem(CONFIG.storageKeys.width);
    if (savedWidth) {
      return parseInt(savedWidth) || CONFIG.defaultWidth;
    }
    return CONFIG.defaultWidth;
  }

  function saveExpandedTopics() {
    try {
      localStorage.setItem(
        CONFIG.storageKeys.expanded, 
        JSON.stringify([...state.expandedTopics])
      );
    } catch (e) {
      console.warn('Failed to save expanded topics');
    }
  }

  function saveSidebarWidth(width) {
    try {
      localStorage.setItem(CONFIG.storageKeys.width, width.toString());
    } catch (e) {
      console.warn('Failed to save sidebar width');
    }
  }

  function saveScrollPosition() {
    const container = document.getElementById('tbc-topics-container');
    if (container) {
      try {
        localStorage.setItem(CONFIG.storageKeys.scroll, container.scrollTop.toString());
      } catch (e) {}
    }
  }

  function restoreScrollPosition() {
    const container = document.getElementById('tbc-topics-container');
    const saved = localStorage.getItem(CONFIG.storageKeys.scroll);
    if (container && saved) {
      container.scrollTop = parseInt(saved) || 0;
    }
  }

  // ================================
  // Sidebar HTML Generation
  // ================================
  function generateSidebarHTML() {
    return `
      <div id="tbc-resize-handle" title="Drag to resize"></div>
      <button id="tbc-sidebar-close" aria-label="Close sidebar">×</button>
      
      <div id="tbc-sidebar-header">
        <a href="index.html" title="The Building Coder Archive">
          <span class="tbc-logo">🏗️</span>
          <span>The Building Coder</span>
        </a>
      </div>
      
      <div id="tbc-search-container">
        <div class="tbc-search-wrapper">
          <span class="tbc-search-icon">🔍</span>
          <input type="text" 
                 id="tbc-search-input" 
                 placeholder="Search post titles..." 
                 autocomplete="off"
                 aria-label="Search post titles">
          <button id="tbc-search-clear" class="hidden" aria-label="Clear search">×</button>
        </div>
        <div class="tbc-search-options">
          <label class="tbc-search-toggle">
            <input type="checkbox" id="tbc-search-content-toggle" disabled title="Loading search index...">
            <span>Search in content</span>
          </label>
        </div>
        <div id="tbc-search-results"></div>
      </div>
      
      <div id="tbc-welcome-link">
        <a href="0001_welcome.htm" title="Welcome to The Building Coder">
          <span class="tbc-welcome-icon">👋</span>
          <span>Welcome</span>
        </a>
      </div>
      
      <div id="tbc-topics-container" aria-label="Topics navigation">
        <div class="tbc-loading-spinner"></div>
      </div>
      
      <div id="tbc-sidebar-footer">
        <nav id="tbc-nav-links" aria-label="Main navigation">
          <!-- Populated dynamically -->
        </nav>
        <div class="tbc-shortcut-hint">
          Press <kbd>/</kbd> to search
        </div>
      </div>
    `;
  }

  function generateNavLinksHTML(navigation) {
    if (!navigation || !navigation.length) return '';
    
    return navigation.map(link => {
      const isActive = state.currentPage === 'index.html' && 
                       window.location.hash === link.href.replace('index.html', '');
      return `<a href="${escapeHtml(link.href)}"${isActive ? ' class="active"' : ''}>${escapeHtml(link.label)}</a>`;
    }).join('');
  }

  function generateTopicsHTML(topics) {
    if (!topics || !topics.length) {
      return '<div class="tbc-error">No topics found</div>';
    }
    
    return topics.map(topic => generateTopicHTML(topic)).join('');
  }

  function generateTopicHTML(topic, isSubTopic = false) {
    const isExpanded = state.expandedTopics.has(topic.id);
    const postCount = topic.posts ? topic.posts.length : 0;
    const hasSubTopics = topic.subTopics && topic.subTopics.length > 0;
    const totalCount = postCount + (hasSubTopics ? topic.subTopics.reduce((sum, st) => sum + (st.posts?.length || 0), 0) : 0);
    
    const topicClass = isSubTopic ? 'tbc-topic tbc-subtopic' : 'tbc-topic';
    const expandedClass = isExpanded ? ' expanded' : '';
    
    let postsHTML = '';
    if (topic.posts && topic.posts.length) {
      postsHTML = topic.posts.map(post => {
        const isCurrent = isCurrentPost(post.file);
        const currentClass = isCurrent ? ' current' : '';
        return `<a href="${escapeHtml(post.file)}" class="tbc-post-link${currentClass}" title="${escapeHtml(post.title)}">${escapeHtml(post.title)}</a>`;
      }).join('');
    }
    
    let subTopicsHTML = '';
    if (hasSubTopics) {
      subTopicsHTML = topic.subTopics.map(st => generateTopicHTML(st, true)).join('');
    }
    
    return `
      <div class="${topicClass}${expandedClass}" data-topic-id="${escapeHtml(topic.id)}">
        <div class="tbc-topic-header" tabindex="0" role="button" aria-expanded="${isExpanded}">
          <span class="tbc-topic-toggle">▶</span>
          <span class="tbc-topic-title">
            <span class="tbc-topic-id">${escapeHtml(topic.id)}</span>
            ${escapeHtml(topic.title)}
          </span>
          <span class="tbc-topic-count">(${totalCount})</span>
        </div>
        <div class="tbc-topic-posts">
          ${postsHTML}
          ${subTopicsHTML}
        </div>
      </div>
    `;
  }

  function isCurrentPost(postFile) {
    if (!postFile || !state.currentPage) return false;
    
    // Handle URLs with anchors
    const postFileBase = postFile.split('#')[0];
    const currentBase = state.currentPage.split('#')[0];
    
    return postFileBase === currentBase || 
           postFileBase === state.currentPage ||
           postFile === state.currentPage;
  }

  // ================================
  // Sidebar Initialization
  // ================================
  async function initSidebar() {
    // Check if sidebar already exists
    if (document.getElementById('tbc-sidebar')) {
      return;
    }
    
    // Detect current page
    state.currentPage = getCurrentPageFile();
    
    // Load persisted state
    const savedWidth = loadPersistedState();
    
    // Create sidebar container
    const sidebar = document.createElement('div');
    sidebar.id = 'tbc-sidebar';
    sidebar.className = 'loading';
    sidebar.innerHTML = generateSidebarHTML();
    
    // Create mobile toggle button
    const mobileToggle = document.createElement('button');
    mobileToggle.id = 'tbc-mobile-toggle';
    mobileToggle.innerHTML = '☰';
    mobileToggle.setAttribute('aria-label', 'Open navigation');
    
    // Create overlay for mobile
    const overlay = document.createElement('div');
    overlay.id = 'tbc-overlay';
    
    // Wrap existing content
    const body = document.body;
    const existingContent = Array.from(body.childNodes);
    
    const contentWrapper = document.createElement('div');
    contentWrapper.id = 'tbc-content';
    
    existingContent.forEach(node => {
      if (node.id !== 'tbc-sidebar' && 
          node.id !== 'tbc-mobile-toggle' && 
          node.id !== 'tbc-overlay') {
        contentWrapper.appendChild(node);
      }
    });
    
    // Add elements to body
    body.innerHTML = '';
    body.appendChild(sidebar);
    body.appendChild(overlay);
    body.appendChild(mobileToggle);
    body.appendChild(contentWrapper);
    body.classList.add('tbc-has-sidebar');
    
    // Set initial width
    sidebar.style.width = savedWidth + 'px';
    contentWrapper.style.marginLeft = savedWidth + 'px';
    
    // Load TOC data
    try {
      state.tocData = await loadTocData();
      renderSidebar();
      sidebar.classList.remove('loading');

      // Load search index (don't block UI)
      if (CONFIG.enableContentSearch) {
        loadSearchIndex().catch(err => {
          console.warn('Search index unavailable, content search disabled:', err);
        });
      }
    } catch (error) {
      renderError(error);
      sidebar.classList.remove('loading');
    }
    
    // Initialize interactions
    initResizeHandle();
    initSearch();
    initTopicToggles();
    initMobileMenu();
    initKeyboardShortcuts();
    
    // Expand topic containing current page
    expandCurrentTopic();
    
    // Restore scroll position
    setTimeout(restoreScrollPosition, 100);
    
    // Save scroll position on scroll
    const topicsContainer = document.getElementById('tbc-topics-container');
    if (topicsContainer) {
      topicsContainer.addEventListener('scroll', debounce(saveScrollPosition, 500));
    }
  }

  function renderSidebar() {
    if (!state.tocData) return;
    
    // Render navigation links
    const navLinksContainer = document.getElementById('tbc-nav-links');
    if (navLinksContainer) {
      navLinksContainer.innerHTML = generateNavLinksHTML(state.tocData.navigation);
    }
    
    // Render topics and archive
    const topicsContainer = document.getElementById('tbc-topics-container');
    if (topicsContainer) {
      let html = '';
      
      // Section header for topics
      html += `
        <div class="tbc-topics-section">
          <div class="tbc-section-header">
            <span class="tbc-section-icon">📑</span>
            <span class="tbc-section-title">Topic Groups</span>
            <span class="tbc-section-count">(${state.tocData.totalPostLinks || 0} posts)</span>
            <button class="tbc-toggle-all" title="Toggle all topics">±</button>
          </div>
          ${generateTopicsHTML(state.tocData.topics)}
        </div>
      `;
      
      topicsContainer.innerHTML = html;
    }
    
    // Re-init topic toggles after rendering
    initTopicToggles();
  }

  function renderError(error) {
    const topicsContainer = document.getElementById('tbc-topics-container');
    if (topicsContainer) {
      topicsContainer.innerHTML = `
        <div class="tbc-error">
          <div class="tbc-error-icon">⚠️</div>
          <div class="tbc-error-message">Failed to load table of contents</div>
          <button class="tbc-retry-btn" onclick="location.reload()">Retry</button>
        </div>
      `;
    }
  }

  // ================================
  // Resize Handle
  // ================================
  function initResizeHandle() {
    const handle = document.getElementById('tbc-resize-handle');
    const sidebar = document.getElementById('tbc-sidebar');
    const content = document.getElementById('tbc-content');
    
    if (!handle || !sidebar || !content) return;
    
    let startX, startWidth;
    
    function onMouseDown(e) {
      state.isResizing = true;
      startX = e.clientX;
      startWidth = sidebar.offsetWidth;
      
      handle.classList.add('dragging');
      document.body.classList.add('tbc-resizing');
      
      document.addEventListener('mousemove', onMouseMove);
      document.addEventListener('mouseup', onMouseUp);
      
      e.preventDefault();
    }
    
    function onMouseMove(e) {
      if (!state.isResizing) return;
      
      const diff = e.clientX - startX;
      let newWidth = startWidth + diff;
      
      // Enforce constraints
      newWidth = Math.max(CONFIG.minWidth, Math.min(CONFIG.maxWidth, newWidth));
      
      sidebar.style.width = newWidth + 'px';
      content.style.marginLeft = newWidth + 'px';
      
      // Update CSS variable
      document.documentElement.style.setProperty('--tbc-sidebar-width', newWidth + 'px');
    }
    
    function onMouseUp() {
      if (!state.isResizing) return;
      
      state.isResizing = false;
      handle.classList.remove('dragging');
      document.body.classList.remove('tbc-resizing');
      
      document.removeEventListener('mousemove', onMouseMove);
      document.removeEventListener('mouseup', onMouseUp);
      
      // Save width
      saveSidebarWidth(sidebar.offsetWidth);
    }
    
    handle.addEventListener('mousedown', onMouseDown);
    
    // Double-click to reset
    handle.addEventListener('dblclick', () => {
      sidebar.style.width = CONFIG.defaultWidth + 'px';
      content.style.marginLeft = CONFIG.defaultWidth + 'px';
      document.documentElement.style.setProperty('--tbc-sidebar-width', CONFIG.defaultWidth + 'px');
      saveSidebarWidth(CONFIG.defaultWidth);
    });
    
    // Touch support
    handle.addEventListener('touchstart', (e) => {
      state.isResizing = true;
      startX = e.touches[0].clientX;
      startWidth = sidebar.offsetWidth;
      handle.classList.add('dragging');
      e.preventDefault();
    });
    
    document.addEventListener('touchmove', (e) => {
      if (!state.isResizing) return;
      
      const touch = e.touches[0];
      const diff = touch.clientX - startX;
      let newWidth = startWidth + diff;
      newWidth = Math.max(CONFIG.minWidth, Math.min(CONFIG.maxWidth, newWidth));
      
      sidebar.style.width = newWidth + 'px';
      content.style.marginLeft = newWidth + 'px';
    });
    
    document.addEventListener('touchend', () => {
      if (state.isResizing) {
        state.isResizing = false;
        handle.classList.remove('dragging');
        saveSidebarWidth(sidebar.offsetWidth);
      }
    });
  }

  // ================================
  // Search
  // ================================
  function initSearch() {
    const input = document.getElementById('tbc-search-input');
    const clearBtn = document.getElementById('tbc-search-clear');
    const contentToggle = document.getElementById('tbc-search-content-toggle');
    
    if (!input) return;
    
    const performSearchDebounced = debounce(performSearch, CONFIG.searchDebounce);
    
    input.addEventListener('input', () => {
      state.searchQuery = input.value;
      clearBtn.classList.toggle('hidden', !input.value);
      performSearchDebounced(input.value);
    });
    
    clearBtn.addEventListener('click', () => {
      input.value = '';
      state.searchQuery = '';
      clearBtn.classList.add('hidden');
      resetSearch();
      input.focus();
    });

    // Content search toggle handler
    if (contentToggle) {
      contentToggle.addEventListener('change', () => {
        state.searchInContent = contentToggle.checked;
        // Save preference
        try {
          localStorage.setItem(CONFIG.storageKeys.searchInContent, contentToggle.checked.toString());
        } catch (e) {
          // Ignore storage errors
        }
        
        // Clean up content match indicators when disabling content search
        if (!contentToggle.checked) {
          removeAllExcerpts();
        }
        
        // Re-run search with new mode
        if (state.searchQuery) {
          performSearch(state.searchQuery);
        }
      });
    }
  }

  function performSearch(query) {
    const resultsDiv = document.getElementById('tbc-search-results');
    query = query.toLowerCase().trim();
    
    if (!query) {
      resetSearch();
      return;
    }
    
    // Use content search if enabled and index is loaded
    if (state.searchInContent && state.searchIndexLoaded) {
      performContentSearch(query);
    } else {
      performTitleSearch(query);
    }
  }

  function performTitleSearch(query) {
    const resultsDiv = document.getElementById('tbc-search-results');
    const topics = document.querySelectorAll('.tbc-topic');
    const posts = document.querySelectorAll('.tbc-post-link');
    let matchCount = 0;
    
    // Search posts
    posts.forEach(post => {
      const title = post.textContent.toLowerCase();
      const matches = title.includes(query);
      
      post.classList.toggle('tbc-search-no-match', !matches);
      
      if (matches) {
        matchCount++;
        highlightText(post, query);
        
        // Expand parent topic
        const topic = post.closest('.tbc-topic');
        if (topic) {
          topic.classList.add('expanded');
          state.expandedTopics.add(topic.dataset.topicId);
        }
      } else {
        removeHighlight(post);
      }
    });
    
    // Search and show/hide topics
    topics.forEach(topic => {
      const topicTitle = topic.querySelector('.tbc-topic-title');
      const topicTitleText = topicTitle ? topicTitle.textContent.toLowerCase() : '';
      const topicMatches = topicTitleText.includes(query);
      const hasVisiblePosts = topic.querySelector('.tbc-post-link:not(.tbc-search-no-match)');
      
      if (topicMatches) {
        topic.classList.remove('tbc-search-no-match');
        topic.classList.add('expanded');
        state.expandedTopics.add(topic.dataset.topicId);
        
        // Show all posts in matching topic
        topic.querySelectorAll('.tbc-post-link').forEach(p => {
          p.classList.remove('tbc-search-no-match');
          matchCount++;
        });
        
        if (topicTitle) highlightText(topicTitle, query);
      } else {
        topic.classList.toggle('tbc-search-no-match', !hasVisiblePosts);
        if (topicTitle) removeHighlight(topicTitle);
      }
    });
    
    // Update results count
    updateResultsCount(matchCount, resultsDiv, false);
  }

  function performContentSearch(query) {
    const resultsDiv = document.getElementById('tbc-search-results');
    const topics = document.querySelectorAll('.tbc-topic');
    const posts = document.querySelectorAll('.tbc-post-link');
    const matchingPosts = new Map(); // Changed from Set to Map for match metadata

    // Track match type counts for result breakdown
    let titleMatchCount = 0;
    let contentOnlyMatchCount = 0;

    // Search in index
    if (state.searchIndex && state.searchIndex.posts) {
      state.searchIndex.posts.forEach(post => {
        const matchType = getMatchType(post, query);

        if (matchType !== MATCH_TYPE.NONE) {
          matchingPosts.set(post.file, {
            matchType: matchType,
            title: post.title,
            excerpt: post.excerpt,
            contentPreview: post.contentPreview
          });
          
          if (matchType === MATCH_TYPE.CONTENT_ONLY) {
            contentOnlyMatchCount++;
          } else {
            titleMatchCount++;
          }
        }
      });
    }

    let matchCount = 0;

    // Update DOM based on matches
    posts.forEach(post => {
      const href = post.getAttribute('href');
      const matchData = matchingPosts.get(href);
      const matches = !!matchData;

      post.classList.toggle('tbc-search-no-match', !matches);

      if (matches) {
        matchCount++;

        // Add content-match class for content-only matches
        if (matchData.matchType === MATCH_TYPE.CONTENT_ONLY) {
          post.classList.add('tbc-content-match');
          
          // Remove any existing excerpt
          const existingExcerpt = post.parentElement.querySelector('.tbc-excerpt');
          if (existingExcerpt) {
            existingExcerpt.remove();
          }
          
          // Generate and insert new excerpt
          const excerptData = generateExcerptForDisplay(matchData, query);
          if (excerptData) {
            const excerptElement = createExcerptElement(excerptData, href);
            post.parentElement.insertBefore(excerptElement, post.nextSibling);
            // Add ARIA description to post link
            post.setAttribute('aria-describedby', excerptElement.id);
          }
        } else {
          post.classList.remove('tbc-content-match');
          // Remove excerpt if match type changed
          const existingExcerpt = post.parentElement.querySelector('.tbc-excerpt');
          if (existingExcerpt) {
            existingExcerpt.remove();
          }
          post.removeAttribute('aria-describedby');
        }

        // Highlight only title (content not visible in sidebar)
        const title = post.textContent.toLowerCase();
        if (title.includes(query)) {
          highlightText(post, query);
        } else {
          removeHighlight(post);
        }

        // Add highlight parameter to link for in-page highlighting
        if (!post.dataset.originalHref) {
          post.dataset.originalHref = href;
        }
        const url = new URL(href, window.location.origin);
        url.searchParams.set('highlight', query);
        post.setAttribute('href', url.pathname + url.search);

        const topic = post.closest('.tbc-topic');
        if (topic) {
          topic.classList.add('expanded');
          state.expandedTopics.add(topic.dataset.topicId);
        }
      } else {
        removeHighlight(post);
        post.classList.remove('tbc-content-match'); // Clean up content match indicator
        post.removeAttribute('aria-describedby');
        // Remove any excerpt
        const existingExcerpt = post.parentElement.querySelector('.tbc-excerpt');
        if (existingExcerpt) {
          existingExcerpt.remove();
        }
        // Restore original href
        if (post.dataset.originalHref) {
          post.setAttribute('href', post.dataset.originalHref);
          delete post.dataset.originalHref;
        }
      }
    });

    // Handle topics
    topics.forEach(topic => {
      const topicTitle = topic.querySelector('.tbc-topic-title');
      const topicTitleText = topicTitle ? topicTitle.textContent.toLowerCase() : '';
      const topicMatches = topicTitleText.includes(query);
      const hasVisiblePosts = topic.querySelector('.tbc-post-link:not(.tbc-search-no-match)');

      if (topicMatches) {
        topic.classList.remove('tbc-search-no-match');
        topic.classList.add('expanded');
        state.expandedTopics.add(topic.dataset.topicId);

        // Show all posts in matching topic
        topic.querySelectorAll('.tbc-post-link').forEach(p => {
          if (p.classList.contains('tbc-search-no-match')) {
            p.classList.remove('tbc-search-no-match');
            matchCount++;
          }
        });

        if (topicTitle) highlightText(topicTitle, query);
      } else {
        topic.classList.toggle('tbc-search-no-match', !hasVisiblePosts);
        if (topicTitle) removeHighlight(topicTitle);
      }
    });

    updateResultsCountWithBreakdown(matchCount, titleMatchCount, contentOnlyMatchCount, resultsDiv);
  }

  /**
   * Update search results count with match type breakdown
   * @param {number} matchCount - Total visible match count
   * @param {number} titleCount - Matches in title (including BOTH)
   * @param {number} contentOnlyCount - Matches in content only
   * @param {HTMLElement} resultsDiv - Results count element
   */
  function updateResultsCountWithBreakdown(matchCount, titleCount, contentOnlyCount, resultsDiv) {
    if (!resultsDiv) return;
    
    if (matchCount === 0) {
      resultsDiv.textContent = 'No posts found';
      resultsDiv.classList.add('no-results');
    } else if (contentOnlyCount === 0) {
      resultsDiv.textContent = `${matchCount} result${matchCount === 1 ? '' : 's'}`;
      resultsDiv.classList.remove('no-results');
    } else {
      resultsDiv.innerHTML = `${matchCount} result${matchCount === 1 ? '' : 's'} ` +
        `<span class="tbc-result-breakdown">(${titleCount} in title, ${contentOnlyCount} in content)</span>`;
      resultsDiv.classList.remove('no-results');
    }
  }

  function updateResultsCount(matchCount, resultsDiv, isContentSearch) {
    if (resultsDiv) {
      if (matchCount === 0) {
        resultsDiv.textContent = 'No posts found';
        resultsDiv.classList.add('no-results');
      } else {
        const mode = isContentSearch ? ' (title + content)' : '';
        resultsDiv.textContent = `${matchCount} result${matchCount === 1 ? '' : 's'}${mode}`;
        resultsDiv.classList.remove('no-results');
      }
    }
  }

  function resetSearch() {
    const resultsDiv = document.getElementById('tbc-search-results');
    const topics = document.querySelectorAll('.tbc-topic');
    const posts = document.querySelectorAll('.tbc-post-link');
    
    // Remove all excerpts and content match indicators
    removeAllExcerpts();
    
    posts.forEach(post => {
      post.classList.remove('tbc-search-no-match');
      removeHighlight(post);
      post.removeAttribute('aria-describedby');
      // Restore original href if modified by content search
      if (post.dataset.originalHref) {
        post.setAttribute('href', post.dataset.originalHref);
        delete post.dataset.originalHref;
      }
    });
    
    topics.forEach(topic => {
      topic.classList.remove('tbc-search-no-match');
      const topicTitle = topic.querySelector('.tbc-topic-title');
      if (topicTitle) removeHighlight(topicTitle);
    });
    
    if (resultsDiv) {
      resultsDiv.textContent = '';
      resultsDiv.classList.remove('no-results');
    }
  }

  function highlightText(element, query) {
    const originalText = element.getAttribute('data-original-text') || element.textContent;
    element.setAttribute('data-original-text', originalText);
    
    const regex = new RegExp(`(${escapeRegex(query)})`, 'gi');
    element.innerHTML = escapeHtml(originalText).replace(
      regex, 
      '<span class="tbc-search-highlight">$1</span>'
    );
  }

  function removeHighlight(element) {
    const originalText = element.getAttribute('data-original-text');
    if (originalText) {
      element.textContent = originalText;
    }
  }

  // ================================
  // Topic Toggles
  // ================================
  function initTopicToggles() {
    const topicHeaders = document.querySelectorAll('.tbc-topic-header');
    
    topicHeaders.forEach(header => {
      // Remove existing listeners
      header.replaceWith(header.cloneNode(true));
    });
    
    // Re-query and add listeners
    document.querySelectorAll('.tbc-topic-header').forEach(header => {
      header.addEventListener('click', (e) => {
        const topic = header.closest('.tbc-topic');
        if (topic) {
          toggleTopic(topic);
        }
      });
      
      header.addEventListener('keydown', (e) => {
        if (e.key === 'Enter' || e.key === ' ') {
          e.preventDefault();
          const topic = header.closest('.tbc-topic');
          if (topic) {
            toggleTopic(topic);
          }
        }
      });
    });
    
    // Toggle-all buttons
    document.querySelectorAll('.tbc-toggle-all').forEach(btn => {
      btn.addEventListener('click', (e) => {
        e.stopPropagation();
        const section = btn.closest('.tbc-topics-section, .tbc-archive-section');
        if (section) {
          toggleAllInSection(section);
        }
      });
    });
  }

  function toggleAllInSection(section) {
    const topics = section.querySelectorAll('.tbc-topic');
    const expandedCount = section.querySelectorAll('.tbc-topic.expanded').length;
    const shouldExpand = expandedCount < topics.length / 2;
    
    topics.forEach(topic => {
      const topicId = topic.dataset.topicId;
      if (shouldExpand) {
        topic.classList.add('expanded');
        if (topicId) state.expandedTopics.add(topicId);
      } else {
        topic.classList.remove('expanded');
        if (topicId) state.expandedTopics.delete(topicId);
      }
      
      const header = topic.querySelector('.tbc-topic-header');
      if (header) {
        header.setAttribute('aria-expanded', shouldExpand);
      }
    });
    
    saveExpandedTopics();
  }

  function toggleTopic(topicElement) {
    const topicId = topicElement.dataset.topicId;
    const isExpanded = topicElement.classList.toggle('expanded');
    
    const header = topicElement.querySelector('.tbc-topic-header');
    if (header) {
      header.setAttribute('aria-expanded', isExpanded);
    }
    
    if (isExpanded) {
      state.expandedTopics.add(topicId);
    } else {
      state.expandedTopics.delete(topicId);
    }
    
    saveExpandedTopics();
  }

  function expandCurrentTopic() {
    if (!state.currentPage) return;
    
    // Find the post link that matches current page
    const currentLink = document.querySelector('.tbc-post-link.current');
    if (currentLink) {
      // Expand all parent topics
      let parent = currentLink.closest('.tbc-topic');
      while (parent) {
        parent.classList.add('expanded');
        const topicId = parent.dataset.topicId;
        if (topicId) {
          state.expandedTopics.add(topicId);
        }
        parent = parent.parentElement.closest('.tbc-topic');
      }
      
      // Scroll to current link
      setTimeout(() => {
        currentLink.scrollIntoView({ block: 'center', behavior: 'smooth' });
      }, 200);
    }
  }

  // ================================
  // Mobile Menu
  // ================================
  function initMobileMenu() {
    const toggle = document.getElementById('tbc-mobile-toggle');
    const sidebar = document.getElementById('tbc-sidebar');
    const overlay = document.getElementById('tbc-overlay');
    const closeBtn = document.getElementById('tbc-sidebar-close');
    
    if (!toggle || !sidebar) return;
    
    function openSidebar() {
      state.isMobileOpen = true;
      sidebar.classList.add('open');
      if (overlay) overlay.classList.add('active');
      toggle.innerHTML = '×';
      toggle.setAttribute('aria-label', 'Close navigation');
    }
    
    function closeSidebar() {
      state.isMobileOpen = false;
      sidebar.classList.remove('open');
      if (overlay) overlay.classList.remove('active');
      toggle.innerHTML = '☰';
      toggle.setAttribute('aria-label', 'Open navigation');
    }
    
    toggle.addEventListener('click', () => {
      if (state.isMobileOpen) {
        closeSidebar();
      } else {
        openSidebar();
      }
    });
    
    if (overlay) {
      overlay.addEventListener('click', closeSidebar);
    }
    
    if (closeBtn) {
      closeBtn.addEventListener('click', closeSidebar);
    }
    
    // Close on navigation
    sidebar.addEventListener('click', (e) => {
      if (e.target.classList.contains('tbc-post-link') || 
          e.target.closest('#tbc-nav-links a')) {
        closeSidebar();
      }
    });
  }

  // ================================
  // Keyboard Shortcuts
  // ================================
  function initKeyboardShortcuts() {
    document.addEventListener('keydown', (e) => {
      const input = document.getElementById('tbc-search-input');
      
      // Press '/' to focus search (like GitHub)
      if (e.key === '/' && document.activeElement !== input) {
        e.preventDefault();
        if (input) input.focus();
      }
      
      // Press Escape to clear and blur
      if (e.key === 'Escape') {
        if (document.activeElement === input) {
          input.value = '';
          state.searchQuery = '';
          resetSearch();
          input.blur();
          
          const clearBtn = document.getElementById('tbc-search-clear');
          if (clearBtn) clearBtn.classList.add('hidden');
        }
        
        // Close mobile menu
        if (state.isMobileOpen) {
          const sidebar = document.getElementById('tbc-sidebar');
          const overlay = document.getElementById('tbc-overlay');
          const toggle = document.getElementById('tbc-mobile-toggle');
          
          if (sidebar) sidebar.classList.remove('open');
          if (overlay) overlay.classList.remove('active');
          if (toggle) {
            toggle.innerHTML = '☰';
            toggle.setAttribute('aria-label', 'Open navigation');
          }
          state.isMobileOpen = false;
        }
      }
    });
  }

  // ================================
  // Initialize on DOM Ready
  // ================================
  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', initSidebar);
  } else {
    initSidebar();
  }

})();


/**
 * Chronological Timeline Column
 * 
 * Adds an integrated right-side timeline navigation with:
 * - Previous/Next post links
 * - Year browser with expandable post lists
 */
(function() {
  'use strict';

  // ================================
  // Configuration
  // ================================
  const CHRONO_CONFIG = {
    dataUrl: 'toc/chrono-data.json',
    storageKeys: {
      expandedYears: 'tbc-chrono-expanded-years'
    }
  };

  // Mobile Sheet Configuration
  const MOBILE_CONFIG = {
    breakpoint: 768,
    landscapeBreakpoint: 896,
    storageKeys: {
      sheetState: 'tbc-chrono-sheet-state',
      selectedYear: 'tbc-chrono-selected-year',
      selectedMonth: 'tbc-chrono-selected-month'
    },
    swipeThreshold: 50,
    animationDuration: 250
  };

  // ================================
  // State
  // ================================
  const chronoState = {
    data: null,
    currentPostNum: null,
    expandedYears: new Set()
  };

  // Mobile Sheet State
  const mobileState = {
    mode: 'collapsed', // 'collapsed' | 'years' | 'months'
    selectedYear: null,
    selectedMonth: null,
    touchStartY: 0,
    isLandscape: false,
    scrollPosition: 0
  };

  // Feature flag for mobile sheet (configurable at runtime)
  // Priority:
  //   1. URL query parameter: ?mobileSheet=true|false
  //   2. localStorage key: 'tbc-enable-mobile-sheet' ("true" | "false")
  //   3. Default: true
  const ENABLE_MOBILE_SHEET = (function() {
    try {
      if (typeof window !== 'undefined') {
        // URL query parameter override
        if (window.location && window.location.search) {
          const params = new URLSearchParams(window.location.search);
          const param = params.get('mobileSheet');
          if (param === 'true' || param === '1') return true;
          if (param === 'false' || param === '0') return false;
        }

        // localStorage override
        if (window.localStorage) {
          const stored = window.localStorage.getItem('tbc-enable-mobile-sheet');
          if (stored === 'true') return true;
          if (stored === 'false') return false;
        }
      }
    } catch (e) {
      // Ignore configuration errors and fall back to default
    }
    return true;
  })();
  // ================================
  // Utility Functions
  // ================================
  function getCurrentPostNumber() {
    const file = window.location.pathname.split('/').pop();
    const match = file.match(/^(\d{4})_/);
    return match ? parseInt(match[1]) : null;
  }

  function truncateTitle(title, maxLength = 50) {
    if (title.length <= maxLength) return title;
    return title.substring(0, maxLength - 3) + '...';
  }

  // ================================
  // Data Loading
  // ================================
  async function loadChronoData() {
    // Try cache first
    const cached = localStorage.getItem('tbc-chrono-data');
    const cacheTime = localStorage.getItem('tbc-chrono-cache-time');
    const ONE_HOUR = 60 * 60 * 1000;
    
    if (cached && cacheTime && (Date.now() - parseInt(cacheTime)) < ONE_HOUR) {
      try {
        return JSON.parse(cached);
      } catch (e) {
        console.warn('Failed to parse cached chrono data');
      }
    }
    
    // Determine base path
    const currentPath = window.location.pathname;
    let basePath = '';
    
    if (currentPath.includes('/a/') && !currentPath.endsWith('/a/') && !currentPath.endsWith('/a/index.html')) {
      basePath = '';
    } else if (currentPath.endsWith('/a/') || currentPath.endsWith('/a/index.html')) {
      basePath = '';
    } else if (currentPath === '/index.html' || currentPath === '/' || currentPath.match(/^\/\d{4}_/)) {
      // Serving directly from a/ directory (local dev or subdomain)
      basePath = '';
    } else {
      basePath = 'a/';
    }
    
    const url = basePath + CHRONO_CONFIG.dataUrl;
    
    try {
      const response = await fetch(url);
      if (!response.ok) throw new Error(`HTTP ${response.status}`);
      
      const data = await response.json();
      
      // Cache the data
      try {
        localStorage.setItem('tbc-chrono-data', JSON.stringify(data));
        localStorage.setItem('tbc-chrono-cache-time', Date.now().toString());
      } catch (e) {
        console.warn('Failed to cache chrono data');
      }
      
      return data;
    } catch (error) {
      console.error('Failed to load chrono-data.json:', error);
      return null;
    }
  }

  // ================================
  // Navigation Helpers
  // ================================
  function findPostByNum(posts, num) {
    return posts.find(p => p.num === num);
  }

  function findPrevNext(posts, currentNum) {
    const index = posts.findIndex(p => p.num === currentNum);
    if (index === -1) return { prev: null, next: null };
    
    return {
      prev: index > 0 ? posts[index - 1] : null,
      next: index < posts.length - 1 ? posts[index + 1] : null
    };
  }

  // ================================
  // Render Timeline Column
  // ================================
  function renderChronoColumn() {
    if (!chronoState.data) return;
    
    const currentNum = chronoState.currentPostNum;
    const { prev, next } = findPrevNext(chronoState.data.posts, currentNum);
    const currentPost = findPostByNum(chronoState.data.posts, currentNum);
    
    // Build HTML
    let html = `
      <nav class="tbc-chrono-nav" aria-label="Chronological navigation">
        <div class="tbc-chrono-prevnext">
    `;
    
    // Previous post
    if (prev) {
      html += `
        <a href="${prev.file}" class="tbc-chrono-prev">
          <span class="tbc-chrono-label">← Previous</span>
          <span class="tbc-chrono-title">#${String(prev.num).padStart(4, '0')}: ${truncateTitle(prev.title, 40)}</span>
        </a>
      `;
    } else {
      html += `
        <span class="tbc-chrono-prev" style="opacity: 0.3;">
          <span class="tbc-chrono-label">← Previous</span>
          <span class="tbc-chrono-title">First post</span>
        </span>
      `;
    }
    
    // Next post
    if (next) {
      html += `
        <a href="${next.file}" class="tbc-chrono-next">
          <span class="tbc-chrono-label">Next →</span>
          <span class="tbc-chrono-title">#${String(next.num).padStart(4, '0')}: ${truncateTitle(next.title, 40)}</span>
        </a>
      `;
    } else {
      html += `
        <span class="tbc-chrono-next" style="opacity: 0.3;">
          <span class="tbc-chrono-label">Next →</span>
          <span class="tbc-chrono-title">Latest post</span>
        </span>
      `;
    }
    
    html += `</div>`;
    
    // Current post indicator
    if (currentPost) {
      html += `
        <div class="tbc-chrono-current">
          <span class="tbc-chrono-label">Current Post</span>
          <span class="tbc-chrono-num">#${String(currentPost.num).padStart(4, '0')} · ${currentPost.date}</span>
        </div>
      `;
    }
    
    // Year browser
    html += `
      <hr class="tbc-chrono-divider">
      <div class="tbc-chrono-years">
        <span class="tbc-chrono-section-label">Browse by Year</span>
        <ul>
    `;
    
    for (const yearInfo of chronoState.data.years) {
      const isExpanded = chronoState.expandedYears.has(yearInfo.year);
      html += `
        <li>
          <div class="tbc-chrono-year-item">
            <a class="tbc-chrono-year-link" data-year="${yearInfo.year}">${yearInfo.year}</a>
            <span class="tbc-chrono-year-count">(${yearInfo.count})</span>
          </div>
          <ul class="tbc-chrono-year-posts ${isExpanded ? 'expanded' : ''}" data-year="${yearInfo.year}">
          </ul>
        </li>
      `;
    }
    
    html += `
        </ul>
      </div>
    </nav>
    `;
    
    // Create column element
    const column = document.createElement('aside');
    column.className = 'tbc-chrono-column';
    column.innerHTML = html;
    
    return column;
  }

  // ================================
  // Year Expansion
  // ================================
  function toggleYear(year) {
    const postsList = document.querySelector(`.tbc-chrono-year-posts[data-year="${year}"]`);
    if (!postsList) return;
    
    const isExpanded = postsList.classList.contains('expanded');
    
    if (isExpanded) {
      postsList.classList.remove('expanded');
      chronoState.expandedYears.delete(year);
    } else {
      // Load posts for this year if not already loaded
      if (!postsList.hasChildNodes() || postsList.children.length === 0) {
        loadYearPosts(year, postsList);
      }
      postsList.classList.add('expanded');
      chronoState.expandedYears.add(year);
    }
    
    // Save state
    saveExpandedYears();
  }

  function loadYearPosts(year, container) {
    const posts = chronoState.data.posts.filter(p => p.year === year);
    const currentNum = chronoState.currentPostNum;
    
    let html = '';
    for (const post of posts.slice().reverse()) { // Show newest first within year
      const isCurrent = post.num === currentNum;
      html += `
        <li>
          <a href="${post.file}" class="tbc-chrono-post-link ${isCurrent ? 'current' : ''}">
            ${String(post.num).padStart(4, '0')}: ${truncateTitle(post.title, 35)}
          </a>
        </li>
      `;
    }
    
    container.innerHTML = html;
  }

  function saveExpandedYears() {
    try {
      const years = Array.from(chronoState.expandedYears);
      localStorage.setItem(CHRONO_CONFIG.storageKeys.expandedYears, JSON.stringify(years));
    } catch (e) {
      console.warn('Failed to save expanded years');
    }
  }

  function loadExpandedYears() {
    try {
      const stored = localStorage.getItem(CHRONO_CONFIG.storageKeys.expandedYears);
      if (stored) {
        const years = JSON.parse(stored);
        chronoState.expandedYears = new Set(years);
      }
    } catch (e) {
      console.warn('Failed to load expanded years');
    }
  }

  // ================================
  // Event Handlers
  // ================================
  function initYearClickHandlers(column) {
    column.addEventListener('click', (e) => {
      const yearLink = e.target.closest('.tbc-chrono-year-link');
      if (yearLink) {
        e.preventDefault();
        const year = parseInt(yearLink.dataset.year);
        toggleYear(year);
      }
    });
  }

  // ================================
  // Keyboard Navigation
  // ================================
  function initKeyboardNav() {
    document.addEventListener('keydown', (e) => {
      // Skip if user is typing in an input
      if (document.activeElement.tagName === 'INPUT' || 
          document.activeElement.tagName === 'TEXTAREA') {
        return;
      }
      
      const { prev, next } = findPrevNext(chronoState.data.posts, chronoState.currentPostNum);
      
      // '[' or left arrow for previous post
      if ((e.key === '[' || (e.key === 'ArrowLeft' && e.altKey)) && prev) {
        window.location.href = prev.file;
      }
      
      // ']' or right arrow for next post  
      if ((e.key === ']' || (e.key === 'ArrowRight' && e.altKey)) && next) {
        window.location.href = next.file;
      }
    });
  }

  // ================================
  // DOM Integration
  // ================================
  function waitForSidebar(maxWait = 5000) {
    return new Promise((resolve) => {
      const content = document.getElementById('tbc-content');
      if (content) {
        resolve(content);
        return;
      }
      
      const startTime = Date.now();
      const interval = setInterval(() => {
        const content = document.getElementById('tbc-content');
        if (content) {
          clearInterval(interval);
          resolve(content);
        } else if (Date.now() - startTime > maxWait) {
          clearInterval(interval);
          resolve(null);
        }
      }, 50);
    });
  }

  function wrapContentWithColumn(column, content) {
    if (!content) {
      console.warn('tbc-content not found, skipping chrono column');
      return false;
    }
    
    // Create wrapper
    const wrapper = document.createElement('div');
    wrapper.className = 'tbc-content-wrapper';
    
    // Create content container
    const contentContainer = document.createElement('div');
    contentContainer.className = 'tbc-blog-content';
    
    // Move existing content into container
    while (content.firstChild) {
      contentContainer.appendChild(content.firstChild);
    }
    
    // Assemble wrapper
    wrapper.appendChild(contentContainer);
    wrapper.appendChild(column);
    
    // Add wrapper to content
    content.appendChild(wrapper);
    
    return true;
  }

  // ================================
  // Mobile Sheet - Viewport Detection
  // ================================
  function isMobileViewport() {
    return window.matchMedia(`(max-width: ${MOBILE_CONFIG.breakpoint}px)`).matches;
  }

  function isLandscape() {
    return window.matchMedia('(orientation: landscape)').matches;
  }

  // ================================
  // Mobile Sheet - Body Scroll Lock
  // ================================
  function setBodyScrollLock(locked) {
    if (locked) {
      mobileState.scrollPosition = window.scrollY;
      document.body.classList.add('tbc-sheet-open');
      document.body.style.top = `-${mobileState.scrollPosition}px`;
    } else {
      document.body.classList.remove('tbc-sheet-open');
      document.body.style.top = '';
      window.scrollTo(0, mobileState.scrollPosition || 0);
    }
  }

  // ================================
  // Mobile Sheet - Overlay Management
  // ================================
  function setOverlayVisible(visible) {
    const overlay = document.querySelector('.tbc-chrono-overlay');
    if (!overlay) return;
    
    if (visible) {
      overlay.classList.add('visible');
    } else {
      overlay.classList.remove('visible');
    }
  }

  // ================================
  // Mobile Sheet - State Management
  // ================================
  function setSheetMode(mode) {
    const sheet = document.querySelector('.tbc-chrono-mobile-sheet');
    if (!sheet) return;

    sheet.classList.remove('collapsed', 'partial', 'expanded');
    mobileState.mode = mode;
    
    switch (mode) {
      case 'collapsed':
        sheet.classList.add('collapsed');
        setBodyScrollLock(false);
        setOverlayVisible(false);
        break;
      case 'years':
        sheet.classList.add('partial');
        setBodyScrollLock(true);
        setOverlayVisible(true);
        renderYearGrid();
        break;
      case 'months':
        sheet.classList.add('expanded');
        setBodyScrollLock(true);
        setOverlayVisible(true);
        renderMonthView();
        break;
    }

    saveMobileSheetState();
  }

  // ================================
  // Mobile Sheet - Year Grid Rendering
  // ================================
  function renderYearGrid() {
    const container = document.querySelector('.tbc-sheet-content');
    if (!container) return;

    if (!chronoState.data || !chronoState.data.years) {
      container.innerHTML = `
        <div class="tbc-sheet-empty-state">
          <div class="tbc-sheet-empty-state-icon">📅</div>
          <div>Navigation unavailable</div>
          <div style="font-size: 12px; margin-top: 8px;">Unable to load post data</div>
        </div>
      `;
      return;
    }

    const years = chronoState.data.years || [];
    
    if (years.length === 0) {
      container.innerHTML = `
        <div class="tbc-sheet-empty-state">
          <div class="tbc-sheet-empty-state-icon">📝</div>
          <div>No posts found</div>
        </div>
      `;
      return;
    }

    const currentYear = chronoState.currentPostNum 
      ? findPostByNum(chronoState.data.posts, chronoState.currentPostNum)?.year 
      : null;

    let html = `
      <div class="tbc-year-grid-header">Browse by Year</div>
      <div class="tbc-year-grid">
    `;

    for (const yearData of years) {
      const isCurrent = yearData.year === currentYear;
      html += `
        <button class="tbc-year-chip ${isCurrent ? 'current' : ''}" 
                data-year="${yearData.year}"
                aria-label="${yearData.year}, ${yearData.count} posts">
          <span class="tbc-year-chip-year">${yearData.year}</span>
          <span class="tbc-year-chip-count">${yearData.count}</span>
        </button>
      `;
    }

    html += '</div>';
    container.innerHTML = html;

    // Add click handlers
    container.querySelectorAll('.tbc-year-chip').forEach(chip => {
      chip.addEventListener('click', () => {
        mobileState.selectedYear = parseInt(chip.dataset.year);
        // Find the newest month with posts for this year
        const yearPosts = chronoState.data.posts.filter(p => p.year === mobileState.selectedYear);
        const months = [...new Set(yearPosts.map(p => p.month))];
        mobileState.selectedMonth = Math.max(...months);
        setSheetMode('months');
      });
    });
  }

  // ================================
  // Mobile Sheet - Month View Rendering
  // ================================
  function renderMonthView() {
    const container = document.querySelector('.tbc-sheet-content');
    if (!container || !chronoState.data || !mobileState.selectedYear) return;

    const year = mobileState.selectedYear;
    const posts = chronoState.data.posts.filter(p => p.year === year);
    
    // Group by month
    const monthGroups = {};
    for (const post of posts) {
      if (!monthGroups[post.month]) {
        monthGroups[post.month] = [];
      }
      monthGroups[post.month].push(post);
    }

    // Sort months descending
    const months = Object.keys(monthGroups)
      .map(m => parseInt(m))
      .sort((a, b) => b - a);

    const selectedMonth = mobileState.selectedMonth || months[0];
    const monthNames = ['', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 
                        'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

    const isLandscapeMode = isLandscape() && window.matchMedia(`(max-width: ${MOBILE_CONFIG.landscapeBreakpoint}px)`).matches;

    let html = `
      <div class="tbc-sheet-header">
        <button class="tbc-sheet-back" aria-label="Back to years">←</button>
        <div class="tbc-sheet-title">
          <span class="tbc-sheet-year">${year}</span>
          <span class="tbc-sheet-count">${posts.length} posts</span>
        </div>
        <button class="tbc-sheet-close" aria-label="Close">✕</button>
      </div>
    `;

    if (isLandscapeMode) {
      // Landscape: side-by-side layout
      html += '<div class="tbc-sheet-content-landscape">';
      html += '<div class="tbc-month-list-sidebar">';
      for (const month of months) {
        const isActive = month === selectedMonth;
        const count = monthGroups[month].length;
        html += `
          <button class="tbc-month-tab ${isActive ? 'active' : ''}" 
                  role="tab"
                  aria-selected="${isActive}"
                  data-month="${month}">
            ${monthNames[month]}
            <span class="tbc-month-tab-count">${count}</span>
          </button>
        `;
      }
      html += '</div><div class="tbc-post-list-main">';
    } else {
      // Portrait: horizontal tabs
      html += '<div class="tbc-month-tabs" role="tablist">';
      for (const month of months) {
        const isActive = month === selectedMonth;
        const count = monthGroups[month].length;
        html += `
          <button class="tbc-month-tab ${isActive ? 'active' : ''}" 
                  role="tab"
                  aria-selected="${isActive}"
                  data-month="${month}">
            ${monthNames[month]}
            <span class="tbc-month-tab-count">${count}</span>
          </button>
        `;
      }
      html += '</div>';
    }

    html += '<div class="tbc-post-list" role="listbox">';

    // Render posts for selected month (newest first)
    const monthPosts = monthGroups[selectedMonth] || [];
    const currentNum = chronoState.currentPostNum;

    for (const post of monthPosts.slice().reverse()) {
      const isCurrent = post.num === currentNum;
      const escapedTitle = escapeHtml(post.title);
      html += `
        <a href="${post.file}" 
           class="tbc-post-item ${isCurrent ? 'current' : ''}"
           role="option"
           aria-selected="${isCurrent}">
          <span class="tbc-post-num">#${String(post.num).padStart(4, '0')}</span>
          <span class="tbc-post-title">${escapedTitle}</span>
          <span class="tbc-post-date">${post.date}</span>
        </a>
      `;
    }

    html += '</div>';

    if (isLandscapeMode) {
      html += '</div></div>'; // Close post-list-main and content-landscape
    }

    container.innerHTML = html;

    // Add event handlers
    container.querySelector('.tbc-sheet-back').addEventListener('click', () => {
      setSheetMode('years');
    });

    container.querySelector('.tbc-sheet-close').addEventListener('click', () => {
      setSheetMode('collapsed');
    });

    container.querySelectorAll('.tbc-month-tab').forEach(tab => {
      tab.addEventListener('click', () => {
        mobileState.selectedMonth = parseInt(tab.dataset.month);
        renderMonthView();
      });
    });

    // Scroll active month tab into view
    const activeTab = container.querySelector('.tbc-month-tab.active');
    if (activeTab && !isLandscapeMode) {
      const prefersReducedMotion = typeof window !== 'undefined'
        && typeof window.matchMedia === 'function'
        && window.matchMedia('(prefers-reduced-motion: reduce)').matches;
      const scrollBehavior = prefersReducedMotion ? 'auto' : 'smooth';
      activeTab.scrollIntoView({ behavior: scrollBehavior, block: 'nearest', inline: 'center' });
    }
  }

  // Helper to escape HTML
  function escapeHtml(text) {
    const div = document.createElement('div');
    div.textContent = text;
    return div.innerHTML;
  }

  // ================================
  // Mobile Sheet - Touch Gesture Handling
  // ================================
  function initTouchGestures(sheet) {
    let startY = 0;
    let currentY = 0;

    sheet.addEventListener('touchstart', (e) => {
      startY = e.touches[0].clientY;
    }, { passive: true });

    sheet.addEventListener('touchmove', (e) => {
      currentY = e.touches[0].clientY;
    }, { passive: true });

    sheet.addEventListener('touchend', () => {
      const deltaY = startY - currentY;
      
      if (Math.abs(deltaY) > MOBILE_CONFIG.swipeThreshold) {
        if (deltaY > 0) {
          // Swipe up - expand
          if (mobileState.mode === 'collapsed') {
            setSheetMode('years');
          }
        } else {
          // Swipe down - collapse
          if (mobileState.mode === 'years') {
            setSheetMode('collapsed');
          } else if (mobileState.mode === 'months') {
            setSheetMode('years');
          }
        }
      }
    });
  }

  // ================================
  // Mobile Sheet - Focus Trap
  // ================================
  function initFocusTrap(container) {
    const focusableSelector = 'button, a[href], input, [tabindex]:not([tabindex="-1"])';
    
    container.addEventListener('keydown', (e) => {
      // Escape key closes sheet
      if (e.key === 'Escape') {
        e.preventDefault();
        setSheetMode('collapsed');
        return;
      }
      
      // Tab trap
      if (e.key === 'Tab') {
        const focusable = container.querySelectorAll(focusableSelector);
        if (focusable.length === 0) return;
        
        const first = focusable[0];
        const last = focusable[focusable.length - 1];
        
        if (e.shiftKey && document.activeElement === first) {
          e.preventDefault();
          last.focus();
        } else if (!e.shiftKey && document.activeElement === last) {
          e.preventDefault();
          first.focus();
        }
      }
    });
  }

  // ================================
  // Mobile Sheet - State Persistence
  // ================================
  function saveMobileSheetState() {
    try {
      localStorage.setItem(MOBILE_CONFIG.storageKeys.sheetState, mobileState.mode);
      if (mobileState.selectedYear) {
        localStorage.setItem(MOBILE_CONFIG.storageKeys.selectedYear, 
                             mobileState.selectedYear.toString());
      }
      if (mobileState.selectedMonth) {
        localStorage.setItem(MOBILE_CONFIG.storageKeys.selectedMonth, 
                             mobileState.selectedMonth.toString());
      }
    } catch (e) {
      console.warn('Failed to save mobile sheet state to localStorage');
      console.warn('Failed to save mobile sheet state to localStorage:', e);
    }
  }

  function loadMobileSheetState() {
    try {
      const mode = localStorage.getItem(MOBILE_CONFIG.storageKeys.sheetState);
      const year = localStorage.getItem(MOBILE_CONFIG.storageKeys.selectedYear);
      const month = localStorage.getItem(MOBILE_CONFIG.storageKeys.selectedMonth);

      if (year) mobileState.selectedYear = parseInt(year);
      if (month) mobileState.selectedMonth = parseInt(month);
      if (mode && ['collapsed', 'years', 'months'].includes(mode)) {
        return mode;
      }
    } catch (e) {
      console.warn('Failed to load mobile sheet state from localStorage:', e);
    }
    return 'collapsed';
  }

  // ================================
  // Mobile Sheet - Context Label Update
  // ================================
  function updateContextLabel() {
    const label = document.querySelector('.tbc-sheet-context');
    if (!label || !chronoState.data) return;

    const currentNum = chronoState.currentPostNum;
    if (currentNum) {
      const post = findPostByNum(chronoState.data.posts, currentNum);
      if (post) {
        const yearPosts = chronoState.data.posts.filter(p => p.year === post.year);
        const yearIndex = yearPosts.findIndex(p => p.num === currentNum) + 1;
        label.textContent = `${post.year} · Post ${yearIndex} of ${yearPosts.length}`;
        return;
      }
    }
    
    label.textContent = `${chronoState.data.posts.length} posts · Tap to browse`;
  }

  // ================================
  // Mobile Sheet - Initialization
  // ================================
  function initMobileSheet() {
    if (!isMobileViewport()) return;

    // Mark body for CSS targeting
    document.body.classList.add('tbc-has-chrono-sheet');

    // Create overlay
    const overlay = document.createElement('div');
    overlay.className = 'tbc-chrono-overlay';
    overlay.addEventListener('click', () => setSheetMode('collapsed'));

    // Create sheet
    const sheet = document.createElement('div');
    sheet.className = 'tbc-chrono-mobile-sheet collapsed';
    sheet.setAttribute('role', 'dialog');
    sheet.setAttribute('aria-label', 'Chronological post navigation');
    sheet.innerHTML = `
      <div class="tbc-sheet-drag-handle"></div>
      <div class="tbc-sheet-collapsed-bar">
        <span class="tbc-sheet-context"></span>
      </div>
      <div class="tbc-sheet-content"></div>
    `;

    document.body.appendChild(overlay);
    document.body.appendChild(sheet);

    // Initialize touch gestures
    initTouchGestures(sheet);

    // Initialize focus trap
    initFocusTrap(sheet);

    // Set up collapsed bar click
    sheet.querySelector('.tbc-sheet-collapsed-bar').addEventListener('click', () => {
      setSheetMode('years');
    });

    // Update context label
    updateContextLabel();

    // Restore state (but start collapsed to avoid jarring experience)
    const savedMode = loadMobileSheetState();
    // Only restore non-collapsed state if there was user selection
    if (savedMode !== 'collapsed' && mobileState.selectedYear) {
      if (savedMode === 'months' && !mobileState.selectedMonth) {
        // Months view requires both a year and a month; fall back to years view if month is missing
        setSheetMode('years');
      } else {
        setSheetMode(savedMode);
      }
    }

    // Handle orientation changes
    window.matchMedia('(orientation: landscape)').addEventListener('change', () => {
      mobileState.isLandscape = isLandscape();
      if (mobileState.mode === 'months') {
        renderMonthView(); // Re-render for layout change
      }
    });

    // Handle viewport resize
    let resizeTimeout;
    window.addEventListener('resize', () => {
      clearTimeout(resizeTimeout);
      resizeTimeout = setTimeout(() => {
        if (!isMobileViewport() && mobileState.mode !== 'collapsed') {
          setSheetMode('collapsed');
        }
      }, 150);
    });

    console.log('Mobile chrono sheet initialized');
  }

  // ================================
  // Initialize
  // ================================
  async function initChronoColumn() {
    
    // Wait for sidebar to create #tbc-content
    const content = await waitForSidebar();
    if (!content) {
      console.warn('Sidebar not initialized, skipping chrono column');
      return;
    }
    
    // Get current post number (null for index.html)
    chronoState.currentPostNum = getCurrentPostNumber();
    
    // Load saved state
    loadExpandedYears();
    
    // Load data
    chronoState.data = await loadChronoData();
    if (!chronoState.data) {
      console.warn('Failed to load chronological data');
      return;
    }
    
    // Render column (works for both index and post pages)
    const column = renderChronoColumn();
    if (!column) return;
    
    // Integrate into DOM
    const success = wrapContentWithColumn(column, content);
    if (!success) return;
    
    // Initialize event handlers
    initYearClickHandlers(column);
    
    // Only init keyboard nav on post pages
    if (chronoState.currentPostNum) {
      initKeyboardNav();
      
      // Auto-expand current year
      const currentPost = findPostByNum(chronoState.data.posts, chronoState.currentPostNum);
      if (currentPost && !chronoState.expandedYears.has(currentPost.year)) {
        toggleYear(currentPost.year);
      }
    } else {
      // On index page, expand the most recent year
      const years = chronoState.data.years || [];
      if (years.length > 0 && !chronoState.expandedYears.has(years[0].year)) {
        toggleYear(years[0].year);
      }
    }
    
    // Initialize mobile sheet if enabled and on mobile viewport
    if (ENABLE_MOBILE_SHEET && isMobileViewport()) {
      initMobileSheet();
    }
    
    console.log('Chrono column initialized');
  }

  // ================================
  // Run on DOM Ready
  // ================================
  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', initChronoColumn);
  } else {
    initChronoColumn();
  }

})();

// ================================
// In-Page Content Highlighting (Independent Module)
// ================================
(function initContentHighlightModule() {
  'use strict';

  const HIGHLIGHT_CONFIG = {
    paramName: 'highlight',
    className: 'tbc-content-highlight',
    maxHighlights: 100,
    scrollToFirst: true,
    animateDuration: 2000
  };

  /**
   * Initialize in-page content highlighting from URL parameter
   */
  function initContentHighlighting() {
    const urlParams = new URLSearchParams(window.location.search);
    const highlightTerm = urlParams.get(HIGHLIGHT_CONFIG.paramName);

    if (!highlightTerm || highlightTerm.trim().length < 2) return;

    const term = highlightTerm.trim();
    console.log(`Highlighting content for: "${term}"`);

    // Wait for content to be ready
    requestAnimationFrame(() => {
      highlightContentMatches(term);
    });
  }

  /**
   * Escape special regex characters
   */
  function escapeRegex(string) {
    return string.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
  }

  /**
   * Highlight all matches of term in the page content
   */
  function highlightContentMatches(term) {
    // Target the main content area (avoid sidebar, nav, code blocks)
    const contentArea = document.querySelector('#tbc-content .tbc-blog-content') ||
                        document.querySelector('#tbc-content article') ||
                        document.querySelector('#tbc-content') ||
                        document.querySelector('article') ||
                        document.body;

    if (!contentArea) {
      console.warn('No content area found for highlighting');
      return;
    }

    // Elements to skip
    const skipSelectors = [
      'script', 'style', 'noscript', 'iframe',
      'pre', 'code',  // Skip code blocks
      '.tbc-sidebar', '#tbc-sidebar',
      '.tbc-chrono-column', '.tbc-chrono-nav',
      '.tbc-mobile-sheet', '.tbc-highlight-counter'
    ];

    // Walk text nodes and highlight matches
    const treeWalker = document.createTreeWalker(
      contentArea,
      NodeFilter.SHOW_TEXT,
      {
        acceptNode: function(node) {
          // Skip empty nodes
          if (!node.textContent.trim()) {
            return NodeFilter.FILTER_REJECT;
          }
          // Skip nodes inside excluded elements
          let parent = node.parentElement;
          while (parent) {
            if (skipSelectors.some(sel => parent.matches && parent.matches(sel))) {
              return NodeFilter.FILTER_REJECT;
            }
            parent = parent.parentElement;
          }
          return NodeFilter.FILTER_ACCEPT;
        }
      }
    );

    const nodesToHighlight = [];
    const regex = new RegExp(`(${escapeRegex(term)})`, 'gi');

    // Collect nodes first (can't modify DOM while walking)
    let node;
    while ((node = treeWalker.nextNode())) {
      if (regex.test(node.textContent)) {
        nodesToHighlight.push(node);
        regex.lastIndex = 0; // Reset regex
      }
    }

    if (nodesToHighlight.length === 0) {
      console.log('No matches found for highlighting');
      return;
    }

    let totalHighlights = 0;
    const allMarks = [];

    // Apply highlights
    nodesToHighlight.forEach(textNode => {
      if (totalHighlights >= HIGHLIGHT_CONFIG.maxHighlights) return;

      const text = textNode.textContent;
      const parts = text.split(regex);

      if (parts.length <= 1) return;

      const fragment = document.createDocumentFragment();

      parts.forEach(part => {
        if (part.match(regex)) {
          if (totalHighlights < HIGHLIGHT_CONFIG.maxHighlights) {
            const mark = document.createElement('mark');
            mark.className = HIGHLIGHT_CONFIG.className;
            mark.textContent = part;
            fragment.appendChild(mark);
            allMarks.push(mark);
            totalHighlights++;
          } else {
            fragment.appendChild(document.createTextNode(part));
          }
        } else {
          fragment.appendChild(document.createTextNode(part));
        }
      });

      textNode.parentNode.replaceChild(fragment, textNode);
    });

    console.log(`Highlighted ${totalHighlights} matches`);

    // Create counter badge
    if (allMarks.length > 0) {
      createHighlightCounter(allMarks, term);

      // Scroll to first match
      if (HIGHLIGHT_CONFIG.scrollToFirst) {
        setTimeout(() => {
          allMarks[0].classList.add('tbc-highlight-pulse');
          allMarks[0].scrollIntoView({ behavior: 'smooth', block: 'center' });
        }, 100);
      }
    }
  }

  /**
   * Create floating counter badge with navigation
   */
  function escapeHtml(str) {
    return String(str)
      .replace(/&/g, '&amp;')
      .replace(/</g, '&lt;')
      .replace(/>/g, '&gt;')
      .replace(/"/g, '&quot;')
      .replace(/'/g, '&#39;');
  }

  function createHighlightCounter(allMarks, term) {
    const counter = document.createElement('div');
    counter.className = 'tbc-highlight-counter';
    const displayTerm = term.length > 20 ? term.substring(0, 20) + '...' : term;
    const escapedTerm = escapeHtml(displayTerm);
    counter.innerHTML = `
      <span class="tbc-highlight-count">${allMarks.length}</span>
      <span class="tbc-highlight-label">matches for "${escapedTerm}"</span>
      <button class="tbc-highlight-nav tbc-highlight-prev" aria-label="Previous match">▲</button>
      <button class="tbc-highlight-nav tbc-highlight-next" aria-label="Next match">▼</button>
      <button class="tbc-highlight-clear" aria-label="Clear highlights">×</button>
    `;
    document.body.appendChild(counter);

    let currentIndex = 0;

    // Keyboard navigation
    function highlightKeyHandler(e) {
      if (e.key === 'Escape') {
        cleanup();
      } else if (e.key === 'F3' || (e.ctrlKey && e.key === 'g')) {
        e.preventDefault();
        if (e.shiftKey) {
          currentIndex = (currentIndex - 1 + allMarks.length) % allMarks.length;
        } else {
          currentIndex = (currentIndex + 1) % allMarks.length;
        }
        scrollToHighlight(allMarks, currentIndex);
      }
    }
    document.addEventListener('keydown', highlightKeyHandler);

    // Cleanup function to remove highlights, counter, and event listener
    function cleanup() {
      clearContentHighlights();
      counter.remove();
      document.removeEventListener('keydown', highlightKeyHandler);
      // Remove highlight param from URL
      const url = new URL(window.location);
      url.searchParams.delete(HIGHLIGHT_CONFIG.paramName);
      window.history.replaceState({}, '', url);
    }

    // Clear button
    counter.querySelector('.tbc-highlight-clear').addEventListener('click', () => {
      cleanup();
    });

    // Navigation buttons
    counter.querySelector('.tbc-highlight-prev').addEventListener('click', () => {
      if (allMarks.length === 0) return;
      currentIndex = (currentIndex - 1 + allMarks.length) % allMarks.length;
      scrollToHighlight(allMarks, currentIndex);
    });

    counter.querySelector('.tbc-highlight-next').addEventListener('click', () => {
      if (allMarks.length === 0) return;
      currentIndex = (currentIndex + 1) % allMarks.length;
      scrollToHighlight(allMarks, currentIndex);
    });
  }

  /**
   * Scroll to a specific highlight
   */
  function scrollToHighlight(allMarks, index) {
    // Remove active class from all
    document.querySelectorAll('.' + HIGHLIGHT_CONFIG.className + '.active')
      .forEach(m => m.classList.remove('active'));

    // Add active class and scroll
    const mark = allMarks[index];
    if (mark) {
      mark.classList.add('active');
      mark.scrollIntoView({ behavior: 'smooth', block: 'center' });
    }
  }

  /**
   * Clear all content highlights
   */
  function clearContentHighlights() {
    const marks = document.querySelectorAll('.' + HIGHLIGHT_CONFIG.className);

    marks.forEach(mark => {
      const text = document.createTextNode(mark.textContent);
      mark.parentNode.replaceChild(text, mark);
    });

    // Normalize to merge adjacent text nodes
    document.body.normalize();
  }

  // Initialize on DOM ready
  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', initContentHighlighting);
  } else {
    initContentHighlighting();
  }

})();