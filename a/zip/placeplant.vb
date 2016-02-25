<Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Automatic)> _
<Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)> _
<Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.NoCommandData)> _
Public Class Command
    Implements Autodesk.Revit.UI.IExternalCommand

    Private Function GetFamilySymbol(ByVal symbolname As String, _
                                     ByVal commandData As Autodesk.Revit.UI.ExternalCommandData) _
                                 As Autodesk.Revit.DB.FamilySymbol
        Dim component As Autodesk.Revit.DB.FamilySymbol = Nothing
        Dim collector As New Autodesk.Revit.DB.FilteredElementCollector(commandData.Application.ActiveUIDocument.Document)
        Dim collection As ICollection(Of Autodesk.Revit.DB.Element) = collector.OfClass(GetType(Autodesk.Revit.DB.FamilySymbol)).ToElements()
        For Each e As Autodesk.Revit.DB.Element In collection
            component = TryCast(e, Autodesk.Revit.DB.FamilySymbol)
            If component IsNot Nothing Then
                If component.Name = symbolname Then
                    Return component
                End If
            End If
        Next
        Return Nothing
    End Function

    Private Function GetSurface(ByVal commandData As Autodesk.Revit.UI.ExternalCommandData) _
                             As Autodesk.Revit.DB.TopographySurface
        Dim component As Autodesk.Revit.DB.TopographySurface = Nothing
        Dim collector As New Autodesk.Revit.DB.FilteredElementCollector(commandData.Application.ActiveUIDocument.Document)
        Dim collection As ICollection(Of Autodesk.Revit.DB.Element) = collector.OfClass(GetType(Autodesk.Revit.DB.TopographySurface)).ToElements()
        For Each e As Autodesk.Revit.DB.Element In collection
            component = TryCast(e, Autodesk.Revit.DB.TopographySurface)
            If component IsNot Nothing Then
                Return component
            End If
        Next
        Return Nothing
    End Function


    Public Function Execute( _
        ByVal commandData As Autodesk.Revit.UI.ExternalCommandData, _
        ByRef message As String, _
        ByVal elements As Autodesk.Revit.DB.ElementSet) _
        As Autodesk.Revit.UI.Result _
        Implements Autodesk.Revit.UI.IExternalCommand.Execute

        Dim filename As String = "C:\ProgramData\Autodesk\RAC 2011\Imperial Library\Planting\RPC Tree - Deciduous.rfa"
        Dim symbolname As String = "Scarlet Oak - 42'"
        If Not commandData.Application.ActiveUIDocument.Document.LoadFamilySymbol(filename, symbolname) Then
            MsgBox(msg02) ' error loading symbol
        End If
        Dim familysymbol As Autodesk.Revit.DB.FamilySymbol = Nothing
        familysymbol = GetFamilySymbol(symbolname, commandData)


        Dim parameters As Autodesk.Revit.DB.ParameterSet = familysymbol.Parameters
        Dim parameter As Autodesk.Revit.DB.Parameter
        For Each parameter In parameters
            If Not parameter.IsReadOnly Then
                If parameter.Definition.Name = "Manufacturer" Then ' botanical
                    parameter.Set("Botanical")
                ElseIf parameter.Definition.Name = "Model" Then ' common
                    parameter.Set("Common")
                ElseIf parameter.Definition.Name = "Keynote" Then ' code
                    parameter.Set("CODE")
                ElseIf parameter.Definition.Name = "Type Comments" Then ' size
                    parameter.Set("Size")
                ElseIf parameter.Definition.Name = "Description" Then ' remarks
                    parameter.Set("Remarks")
                ElseIf parameter.Definition.Name = "Type Mark" Then ' detail
                    parameter.Set("Detail")
                ElseIf parameter.Definition.Name = "URL" Then ' url
                    parameter.Set("http://URL.com")
                ElseIf parameter.Definition.Name = "Cost" Then ' cost
                    parameter.Set(666.66)
                End If
            End If
        Next

        Dim point As New Autodesk.Revit.DB.XYZ(0, 0, 0)
        Dim surface As Autodesk.Revit.DB.TopographySurface = GetSurface(commandData)
        Dim stype As Autodesk.Revit.DB.Structure.StructuralType = DB.Structure.StructuralType.NonStructural

        Dim plantx As Autodesk.Revit.DB.FamilyInstance = commandData.Application.ActiveUIDocument.Document.Create.NewFamilyInstance _
            (point, familysymbol, surface, stype)
        ' @@ document.parameterset, as bindingclass, to make new parameters
        For Each parameter In parameters
            If Not parameter.IsReadOnly Then
                If parameter.Definition.Name = "Mark" Then
                    parameter.Set("HANDLE")
                End If
            End If
        Next

        Return Autodesk.Revit.UI.Result.Succeeded

    End Function

End Class
