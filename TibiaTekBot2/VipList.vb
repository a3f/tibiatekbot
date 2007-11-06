Public Module VipListModule

    Public Class VipList
        Private Position As Integer

        Public Sub New()
            Position = 0
        End Sub

        Public ReadOnly Property GetID() As Integer
            Get
                Try
                    Dim ID As Integer = 0
                    Core.ReadMemory(Consts.ptrVipListBegin + (Position * Consts.VipDist), ID, 4)
                    Return CUInt(ID)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property GetName() As String
            Get
                Try
                    Dim Name As String = ""
                    Dim Address As Integer = Consts.ptrVipListBegin + (Position * Consts.VipDist) + Consts.VipNameOffset
                    Core.ReadMemory(Address, Name)
                    Return Name
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property IsOnline() As Boolean
            Get
                Try
                    Dim Online As Integer = 0
                    Core.ReadMemory(Consts.ptrVipListBegin + (Position * Consts.VipDist) + Consts.VipStatusOffset, Online, 1)
                    Return (Online > 0)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public Function Find(ByVal Name As String, Optional ByVal MustBeOnline As Boolean = False, Optional ByVal Offset As Integer = 0) As Boolean
            Try
                For I As Integer = Offset To Consts.VipMax - 1
                    If GetID = 0 Then Continue For
                    If String.Compare(Name, GetName, True) = 0 Then
                        If MustBeOnline Then
                            Return IsOnline
                        Else
                            Return True
                        End If
                    End If
                Next
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function Reset(Optional ByVal MustBeOnline As Boolean = False) As Boolean
            Try
                If Not MustBeOnline Then
                    Position = 0
                    Return True
                Else
                    For Position = 0 To Consts.VipMax - 1
                        If IsOnline Then Return True
                    Next
                End If
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function NextPlayer(Optional ByVal MustBeOnline As Boolean = False) As Boolean
            Try
                If Not MustBeOnline Then
                    Position += 1
                    If Position = Consts.VipMax Then
                        Position -= 1
                        Return False
                    Else
                        Return True
                    End If

                Else
                    For Position = Position + 1 To Consts.VipMax - 1
                        If IsOnline Then Return True
                    Next
                End If
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

    End Class

End Module
