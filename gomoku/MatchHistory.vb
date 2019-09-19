Public Class MatchHistory
    Public Shared MatchAvail As Boolean     'public variables are shared amongst all game modes
    Public Shared ModeNum As Integer
    Private Sub MatchHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MatchAvail = True And ModeNum = 1 Then       'When a match is completed in Two Player Mode, the values are returned and the stored data is displayed on the listbox 

            lbxMatch.Items.Add(TwoPlayer.Data(TwoPlayer.DataNum))
            TwoPlayer.DataNum += 1
            MatchAvail = False
        ElseIf MatchAvail = True And ModeNum = 2 Then       'When a match is completed in HardMode, the values are returned and the stored data is displayed on the listbox 
            lbxMatch.Items.Add(SinglePlayer.Data(SinglePlayer.DataNum))
            SinglePlayer.DataNum += 1
            MatchAvail = False
        ElseIf MatchAvail = True And ModeNum = 3 Then       'When a match is completed in HardMode, the values are returned and the stored data is displayed on the listbox 
            lbxMatch.Items.Add(HardMode.Data(HardMode.DataNum))
            HardMode.DataNum += 1
            MatchAvail = False
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        For i = 0 To HardMode.DataNum               'clear List box
            lbxMatch.Items.Clear()
        Next
    End Sub
End Class