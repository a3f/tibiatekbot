Imports System.xml

Public Module OutfitsModule

    Public OutfitDefinitions As New OutfitDefinition

    Public Structure OutfitDefinition
        Dim ID As UShort
        Dim Name As String
    End Structure

    Public Class Outfits
        Private OutfitsList As New List(Of OutfitDefinition)

        Public ReadOnly Property GetOutfits() As OutfitDefinition()
            Get
                Try
                    Dim OutfitsArray(OutfitsList.Count) As OutfitDefinition
                    OutfitsList.CopyTo(OutfitsArray)
                    Return OutfitsArray
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public Function GetOutfitByName(ByVal Name As String, ByRef Outfit As OutfitDefinition) As Boolean
            Try
                For Each TempOutfit As OutfitDefinition In OutfitsList
                    If String.Compare(TempOutfit.Name, Name, True) = 0 Then
                        Outfit = TempOutfit
                        Return True
                    End If
                Next
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function GetOutfitByID(ByVal ID As UShort, ByRef Outfit As OutfitDefinition) As Boolean
            Try
                For Each TempOutfit As OutfitDefinition In OutfitsList
                    If TempOutfit.ID = ID Then
                        Outfit = TempOutfit
                        Return True
                    End If
                Next
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Sub New()
            LoadOutfits()
        End Sub

        Public Sub LoadOutfits()
            Dim Reader As New System.Xml.XmlTextReader(GetConfigurationDirectory() & "\Outfits.xml")
            Dim Outfit As OutfitDefinition
            Dim Value As String
            OutfitsList.Clear()
            Reader.WhitespaceHandling = WhitespaceHandling.None
            While Reader.Read()
                If Reader.NodeType = XmlNodeType.Element Then
                    Select Case Reader.Name
                        Case "Outfits"
                            While Reader.Read()
                                If Reader.NodeType = XmlNodeType.Element AndAlso Reader.Name = "Outfit" Then
                                    If Reader.HasAttributes Then
                                        Value = Reader.GetAttribute("ID")
                                        If Value.Length > 0 AndAlso Value.Chars(0) = "H" Then Value = "&" + Value
                                        Outfit.ID = CUShort(Value)
                                        Outfit.Name = Reader.GetAttribute("Name")
                                        OutfitsList.Add(Outfit)
                                    End If
                                ElseIf Reader.NodeType = XmlNodeType.EndElement AndAlso Reader.Name = "Outfits" Then
                                    Exit While
                                End If
                            End While
                    End Select
                End If
            End While
        End Sub

    End Class

End Module
