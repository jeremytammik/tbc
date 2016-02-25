/*
 * $Id: SVGCanvasElement.js 778 2009-08-04 09:53:53Z klaus $
 *
 * This is a simple helper library to use canvas in SVG. It creates
 * canvas elements within foreignObjects
 *
 * Example:
 *
 * var canvas = new SVGCanvasElement(0,0,300,150,parentNode);
 * var context = canvas.getContext('2d');
 * context.fillRect(0,0,300,150);
 *
 * @param {float} dx: upper-left x-coordinate of canvas-area
 * @param {float} dy: upper-left y-coordinate of canvas-area
 * @param {float} dw: overall width of canvas-area
 * @param {float} dh: overall height of canvas-area
 * @param {SVGElement} parentNode: append foreignObject with canvas to this node (optional)
 *                                 defaults to document.rootElement if not specified
 * @return {SVGCanvasElement}: object that implements the HTMLCanvasElement interface
 *
 */

(function(){
    // namespaces
    var svgNS = 'http://www.w3.org/2000/svg';
    var xhtmlNS = 'http://www.w3.org/1999/xhtml';
    var xlinkNS = 'http://www.w3.org/1999/xlink';

    var SVGCanvasElement_ = function(dx,dy,dw,dh,parentNode) {
        // create <foreignObject /> SVG-element
        var f = document.createElementNS(svgNS,"foreignObject");
        f.x.baseVal.value = dx;
        f.y.baseVal.value = dy;
        f.width.baseVal.value = dw;
        f.height.baseVal.value = dh;

        // create <canvas /> XHTML-element
        var c = document.createElementNS(xhtmlNS,"canvas");
        c.width = dw;
        c.height = dh;

        // append foreignObject to parent-node and canvas to foreignObject
        var pNode = (typeof parentNode != 'undefined') ? parentNode : document.rootElement;
        var foObj = pNode.appendChild(f);
        var cElem = foObj.appendChild(c);

        // expose HTMLCanvasElement interface
        this.canvas = cElem;
        this.width = cElem.width;
        this.height = cElem.height;
        this.toDataURL = function(type) {
            // TODO: implement variadic args
            return cElem.toDataURL(type);
        };
        this.getContext = function(contextId) {
            return cElem.getContext(contextId);
        };

       /*
        * This custom method adds an HTMLImageElement to the canvas' foreignObject
        * It expects the ID of an existing SVGImage or a reference to an SVGImage node as argument.
        * In both cases, the xlink:href is used as src for the appended HTMLImageElement.
        *
        * Examples:
        * context.drawImage(canvas.importImage(svgImageRef),0,0);
        * context.drawImage(canvas.importImage('svgImage'),0,0);
        *
        * @param {string|SVGImageElement} sourceImg: ID of SVGImageElement or reference to an SVGImageElement 
        * @return {HTMLImageElement}: reference to the html img-element created in foreignObject
        *
        */
        this.importImage = function(sourceImg) {
            var imgSrc = (typeof sourceImg == 'string')
                ? document.getElementById(sourceImg).getAttributeNS(xlinkNS,"href")
                : sourceImg.getAttributeNS(xlinkNS,"href");
            var img = document.createElementNS(xhtmlNS,"img");
            img.setAttributeNS(null,"src",imgSrc);
            img.style.visibility = "hidden";
            return foObj.appendChild(img);
        };
    }
    window.SVGCanvasElement = SVGCanvasElement_;
})();
