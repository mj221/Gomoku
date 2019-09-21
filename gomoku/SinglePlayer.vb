Public Class SinglePlayer                   'The algorithm for Single Player (Easy) is the same as the algorithm written in HardMode Form
    Public Shared Data(50) As String        'Most of these algorithms are already explained in the HardMode Form
    Public Shared DataNum As Integer        'The only difference is the MatchNum values and the absence of Ai3
    Private Structure Placement
        Public avail As Boolean
    End Structure
    Dim Board(19, 19) As Button
    Dim Timer As Integer = 0
    Dim count As Integer = 0
    Dim placementlimit = 0
    Dim altx2 As Integer
    Dim alty2 As Integer
    Dim opx, opy As Integer
    Dim emptyspace(20, 20) As Placement
    Dim UserWin As Boolean
    Dim possiblepr As Integer
    Dim CheckResult As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        creategame()
        lblUserName.Text = "Player " & SingleMenu.username & " vs. " & "Computer (Easy)"
    End Sub

    Public Sub creategame()
        For i = 1 To 19
            For j = 1 To 19
                Board(i, j) = New Button
                emptyspace(i, j).avail = True
                Board(i, j).Text = ""
                Board(i, j).Name = i & ":" & j
                Board(i, j).BackColor = Color.RosyBrown
                Board(i, j).FlatStyle = FlatStyle.Flat
                Board(i, j).FlatAppearance.BorderColor = Color.Black
                Board(i, j).Top = 27 + (20 * i)
                Board(i, j).Left = 50 + (20 * j)
                Board(i, j).Width = 20
                Board(i, j).Height = 20
                Controls.Add(Board(i, j))

                AddHandler Board(i, j).Click, AddressOf board_click
            Next
        Next

    End Sub
    Private Sub board_click(sender As Object, e As EventArgs)

        sender.Text = "X"
        sender.enabled = False
        My.Computer.Audio.Play(My.Resources.Stone, AudioPlayMode.Background)
        btnUndo.Enabled = True
        btnRedo.Enabled = True
        sender.BackColor = Color.Black
        Dim alternate As String() = sender.name.Split(":")
        Dim alty = alternate(0)
        Dim altx = alternate(1)

        alty2 = alty
        altx2 = altx
        emptyspace(alty2, altx2).avail = False


        checkwin()
        If CheckResult = False Then
            Call Ai2()
        Else
            tmrGame.Enabled = False
            Dim result = MessageBox.Show("Click 'Yes' to Main Menu or 'No' to Continue Playing", "Are you sure?", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                tmrGame.Enabled = False
                If UserWin = True Then
                    Data(DataNum) = SingleMenu.username & " vs. " & "Computer (Easy)" & "  |  " & "Win " & "Timer: " & Timer & " seconds"
                    MatchHistory.MatchAvail = True
                    MatchHistory.ModeNum = 2
                ElseIf UserWin = False Then
                    Data(DataNum) = SingleMenu.username & " vs. " & "Computer (Easy)" & "  |  " & "Lose " & "Timer: " & Timer & " seconds"
                    MatchHistory.MatchAvail = True
                    MatchHistory.ModeNum = 2
                End If
                MainMenu.Show()
                Me.Close()
            ElseIf result = DialogResult.No Then
                MessageBox.Show("The latest move made will be undone")
                Board(alty2, altx2).Enabled = True
                Board(alty2, altx2).Text = ""
                Board(alty2, altx2).BackColor = Color.RosyBrown
                Board(opy, opx).Enabled = True
                Board(opy, opx).BackColor = Color.RosyBrown
                Board(opy, opx).Text = ""
                tmrGame.Enabled = True
                CheckResult = False
            End If
        End If

    End Sub

    Public Sub Ai()
        Dim row, col As Integer
        Randomize()
        row = Int(Rnd() * 19 + 1)
        col = Int(Rnd() * 19 + 1)
        While Board(row, col).Text = "X" Or Board(row, col).Text = " "
            row = Int(Rnd() * 19 + 1)
            col = Int(Rnd() * 19 + 1)
        End While
        Board(row, col).Text = " "
        Board(row, col).Enabled = False
        Board(row, col).BackColor = Color.White
        emptyspace(row, col).avail = False
    End Sub
    Public Sub Ai2()
        If count = 0 Then
            Randomize()
            possiblepr = Int(Rnd() * 8 + 1)

        End If
        Select Case possiblepr
            Case 1  'west
                If emptyspace(alty2, altx2 - 1).avail = False Then
                    count = 0
                    Call Ai()
                ElseIf Board(alty2, altx2 - 1).Text = "" Then
                    Board(alty2, altx2 - 1).Text = " "
                    Board(alty2, altx2 - 1).Enabled = False
                    Board(alty2, altx2 - 1).BackColor = Color.White
                    opy = alty2
                    opx = altx2 - 1

                    emptyspace(alty2, altx2 - 1).avail = False
                    checkwin()
                End If
            Case 2  'north west
                If emptyspace(alty2 - 1, altx2 - 1).avail = False Then
                    count = 0
                    Call Ai()

                ElseIf Board(alty2 - 1, altx2 - 1).Text = "" Then
                    Board(alty2 - 1, altx2 - 1).Text = " "
                    Board(alty2 - 1, altx2 - 1).Enabled = False
                    Board(alty2 - 1, altx2 - 1).BackColor = Color.White
                    opy = alty2 - 1
                    opx = altx2 - 1
                    emptyspace(alty2 - 1, altx2 - 1).avail = False
                    checkwin()
                End If
            Case 3   'north
                If emptyspace(alty2 - 1, altx2).avail = False Then
                    count = 0
                    Call Ai()

                ElseIf Board(alty2 - 1, altx2).Text = "" Then
                    Board(alty2 - 1, altx2).Text = " "
                    Board(alty2 - 1, altx2).Enabled = False
                    Board(alty2 - 1, altx2).BackColor = Color.White
                    opy = alty2 - 1
                    opx = altx2
                    emptyspace(alty2 - 1, altx2).avail = False
                    checkwin()
                End If
            Case 4  'north east
                If emptyspace(alty2 - 1, altx2 + 1).avail = False Then
                    count = 0
                    Call Ai()

                ElseIf Board(alty2 - 1, altx2 + 1).Text = "" Then
                    Board(alty2 - 1, altx2 + 1).Text = " "
                    Board(alty2 - 1, altx2 + 1).Enabled = False
                    Board(alty2 - 1, altx2 + 1).BackColor = Color.White
                    opy = alty2 - 1
                    opx = altx2 + 1
                    emptyspace(alty2 - 1, altx2 + 1).avail = False
                    checkwin()
                End If
            Case 5    'east
                If emptyspace(alty2, altx2 + 1).avail = False Then
                    count = 0
                    Call Ai()

                ElseIf Board(alty2, altx2 + 1).Text = "" Then
                    Board(alty2, altx2 + 1).Text = " "
                    Board(alty2, altx2 + 1).Enabled = False
                    Board(alty2, altx2 + 1).BackColor = Color.White
                    opy = alty2
                    opx = altx2 + 1
                    emptyspace(alty2, altx2 + 1).avail = False
                    checkwin()
                End If
            Case 6  'south east
                If emptyspace(alty2 + 1, altx2 + 1).avail = False Then
                    count = 0
                    Call Ai()

                ElseIf Board(alty2 + 1, altx2 + 1).Text = "" Then
                    Board(alty2 + 1, altx2 + 1).Text = " "
                    Board(alty2 + 1, altx2 + 1).Enabled = False
                    Board(alty2 + 1, altx2 + 1).BackColor = Color.White
                    opy = alty2 + 1
                    opx = altx2 + 1
                    emptyspace(alty2 + 1, altx2 + 1).avail = False
                    checkwin()
                End If
            Case 7   'south
                If emptyspace(alty2 + 1, altx2).avail = False Then
                    count = 0
                    Call Ai()

                ElseIf Board(alty2 + 1, altx2).Text = "" Then
                    Board(alty2 + 1, altx2).Text = " "
                    Board(alty2 + 1, altx2).Enabled = False
                    Board(alty2 + 1, altx2).BackColor = Color.White
                    opy = alty2 + 1
                    opx = altx2
                    emptyspace(alty2 + 1, altx2).avail = False
                    checkwin()
                End If
            Case 8  'south west
                If emptyspace(alty2 + 1, altx2 - 1).avail = False Then
                    count = 0
                    Call Ai()

                ElseIf Board(alty2 + 1, altx2 - 1).Text = "" Then
                    Board(alty2 + 1, altx2 - 1).Text = " "
                    Board(alty2 + 1, altx2 - 1).Enabled = False
                    Board(alty2 + 1, altx2 - 1).BackColor = Color.White
                    opy = alty2 + 1
                    opx = altx2 - 1
                    emptyspace(alty2 + 1, altx2 - 1).avail = False
                    checkwin()
                End If
        End Select
    End Sub

    Public Sub checkwin()
        placementlimit += 1
        For i = 1 To 15
            For j = 1 To 15
                If Board(i, j).Text = "X" And Board(i + 1, j + 1).Text = "X" And Board(i + 2, j + 2).Text = "X" And Board(i + 3, j + 3).Text = "X" And Board(i + 4, j + 4).Text = "X" Then
                    MessageBox.Show("User " & SingleMenu.username & " Win!")
                    UserWin = True
                    CheckResult = True
                End If
            Next
        Next
        For i = 1 To 15
            For j = 1 To 19
                If Board(i, j).Text = "X" And Board(i + 1, j).Text = "X" And Board(i + 2, j).Text = "X" And Board(i + 3, j).Text = "X" And Board(i + 4, j).Text = "X" Then
                    MessageBox.Show("User " & SingleMenu.username & " Win!")
                    UserWin = True
                    CheckResult = True
                End If
            Next
        Next
        For i = 1 To 19
            For j = 1 To 15
                If Board(i, j).Text = "X" And Board(i, j + 1).Text = "X" And Board(i, j + 2).Text = "X" And Board(i, j + 3).Text = "X" And Board(i, j + 4).Text = "X" Then
                    MessageBox.Show("User " & SingleMenu.username & " Win!")
                    UserWin = True
                    CheckResult = True
                End If
            Next
        Next
        For i = 5 To 15
            For j = 5 To 19
                If Board(i, j).Text = "X" And Board(i + 1, j - 1).Text = "X" And Board(i + 2, j - 2).Text = "X" And Board(i + 3, j - 3).Text = "X" And Board(i + 4, j - 4).Text = "X" Then
                    MessageBox.Show("User " & SingleMenu.username & " Win!")
                    UserWin = True
                    CheckResult = True
                End If
            Next
        Next
        'for O
        For i = 1 To 15
            For j = 1 To 15
                If Board(i, j).Text = " " And Board(i + 1, j + 1).Text = " " And Board(i + 2, j + 2).Text = " " And Board(i + 3, j + 3).Text = " " And Board(i + 4, j + 4).Text = " " Then
                    UserWin = False
                    MessageBox.Show("Computer Wins!")
                    CheckResult = True
                End If
            Next
        Next
        For i = 1 To 15
            For j = 1 To 19
                If Board(i, j).Text = " " And Board(i + 1, j).Text = " " And Board(i + 2, j).Text = " " And Board(i + 3, j).Text = " " And Board(i + 4, j).Text = " " Then
                    UserWin = False
                    MessageBox.Show("Computer Wins!")
                    CheckResult = True
                End If
            Next
        Next
        For i = 1 To 19
            For j = 1 To 15
                If Board(i, j).Text = " " And Board(i, j + 1).Text = " " And Board(i, j + 2).Text = " " And Board(i, j + 3).Text = " " And Board(i, j + 4).Text = " " Then
                    UserWin = False
                    MessageBox.Show("Computer Wins!")
                    CheckResult = True
                End If
            Next
        Next
        For i = 5 To 15
            For j = 5 To 19
                If Board(i, j).Text = " " And Board(i + 1, j - 1).Text = " " And Board(i + 2, j - 2).Text = " " And Board(i + 3, j - 3).Text = " " And Board(i + 4, j - 4).Text = " " Then
                    UserWin = False
                    MessageBox.Show("Computer Wins!")
                    CheckResult = True
                End If
            Next
        Next
        If placementlimit = 180 Then
            MessageBox.Show("You have reached the maximum number of placement available (In this situation, this match won't be listed in 'Match History'. You can restart the game after clicking OK to Confirm")
            CheckResult = False
            For i = 1 To 19
                For j = 1 To 19
                    Board(i, j).Text = ""
                    Board(i, j).Enabled = True
                    Board(i, j).BackColor = Color.RosyBrown
                Next
            Next
            btnUndo.Enabled = False
            btnRedo.Enabled = False
            tmrGame.Enabled = True
            Timer = 0
        End If
    End Sub
    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        MessageBox.Show("This function has not been properly implemented in the current version of GOMOKU v.1", "Notice")
        Dim saveFileDialog1 As New SaveFileDialog()
        saveFileDialog1.Filter = "XML File|*.xml|All Files|*."
        saveFileDialog1.Title = "Save Game State"
        saveFileDialog1.ShowDialog()
    End Sub
    Private Sub LoadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadToolStripMenuItem.Click
        MessageBox.Show("This function has not been properly implemented in the current version of GOMOKU v.1", "Notice")
        Dim OpenFileDialog1 As New OpenFileDialog()
        OpenFileDialog1.Filter = "XML File|*.xml|All Files|*."
        OpenFileDialog1.Title = "Load Game State"
        OpenFileDialog1.ShowDialog()
    End Sub
    Private Sub tmrGame_Tick(sender As Object, e As EventArgs) Handles tmrGame.Tick
        Timer += 1
        lblTImer.Text = "Time Elapsed: " & Timer & " seconds"
    End Sub
    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        Help.Show()
    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        My.Computer.Audio.Play(My.Resources.Button, AudioPlayMode.Background)
        Dim result = MessageBox.Show(" Do you wish to go to the Game Menu? All progress will be lost.", "Confirm", MessageBoxButtons.YesNo)
        tmrGame.Enabled = False
        If result = DialogResult.Yes Then

            GameMode.Show()
            Me.Close()
        Else
            tmrGame.Enabled = True
        End If
    End Sub
    Private Sub btnMainMenu_Click(sender As Object, e As EventArgs) Handles btnMainMenu.Click
        My.Computer.Audio.Play(My.Resources.Button, AudioPlayMode.Background)
        Dim result = MessageBox.Show(" Do you wish to go to the Main Menu? All progress will be lost.", "Confirm", MessageBoxButtons.YesNo)
        tmrGame.Enabled = False
        If result = DialogResult.Yes Then

            MainMenu.Show()
            Me.Close()
        Else
            tmrGame.Enabled = True
        End If
    End Sub


    Private Sub ResetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetToolStripMenuItem.Click
        CheckResult = False
        For i = 1 To 19
            For j = 1 To 19
                Board(i, j).Text = ""
                Board(i, j).Enabled = True
                Board(i, j).BackColor = Color.RosyBrown
            Next
        Next
        btnUndo.Enabled = False
        btnRedo.Enabled = False
        tmrGame.Enabled = True
        Timer = 0



    End Sub

    Private Sub MainMenuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MainMenuToolStripMenuItem.Click
        Dim result = MessageBox.Show(" Do you wish to go to the Main Menu? All progress will be lost.", "Confirm", MessageBoxButtons.YesNo)
        tmrGame.Enabled = False
        If result = DialogResult.Yes Then

            MainMenu.Show()
            Me.Close()
        Else
            tmrGame.Enabled = True
        End If
    End Sub

    Private Sub GameMenuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GameMenuToolStripMenuItem.Click
        Dim result = MessageBox.Show(" Do you wish to go to the Game Menu? All progress will be lost.", "Confirm", MessageBoxButtons.YesNo)
        tmrGame.Enabled = False
        If result = DialogResult.Yes Then

            MainMenu.Show()
            Me.Close()
        Else
            tmrGame.Enabled = True
        End If
    End Sub

    Private Sub btnUndo_Click(sender As Object, e As EventArgs) Handles btnUndo.Click
        My.Computer.Audio.Play(My.Resources.Button, AudioPlayMode.Background)
        Board(alty2, altx2).Enabled = True
        Board(alty2, altx2).Text = ""
        Board(alty2, altx2).BackColor = Color.RosyBrown
        emptyspace(alty2, altx2).avail = True
        Board(opy, opx).Enabled = True
        Board(opy, opx).Text = ""
        Board(opy, opx).BackColor = Color.RosyBrown
        emptyspace(opy, opx).avail = True
    End Sub

    Private Sub UndoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UndoToolStripMenuItem.Click
        Board(alty2, altx2).Enabled = True
        Board(alty2, altx2).Text = ""
        Board(alty2, altx2).BackColor = Color.RosyBrown
        emptyspace(alty2, altx2).avail = True
        Board(opy, opx).Enabled = True
        Board(opy, opx).Text = ""
        Board(opy, opx).BackColor = Color.RosyBrown
        emptyspace(opy, opx).avail = True
    End Sub

    Private Sub RedoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RedoToolStripMenuItem.Click
        Board(alty2, altx2).Enabled = False
        emptyspace(alty2, altx2).avail = False
        Board(alty2, altx2).Text = "X"
        Board(alty2, altx2).BackColor = Color.Black
        Board(opy, opx).Enabled = False
        Board(opy, opx).Text = " "
        Board(opy, opx).BackColor = Color.White
        emptyspace(opy, opx).avail = False
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        My.Computer.Audio.Play(My.Resources.Background, AudioPlayMode.BackgroundLoop)
    End Sub

    Private Sub btnRedo_Click(sender As Object, e As EventArgs) Handles btnRedo.Click
        My.Computer.Audio.Play(My.Resources.ting, AudioPlayMode.Background)
        Board(alty2, altx2).Enabled = False
        emptyspace(alty2, altx2).avail = False
        Board(alty2, altx2).Text = "X"
        Board(alty2, altx2).BackColor = Color.Black
        Board(opy, opx).Enabled = False
        Board(opy, opx).Text = " "
        Board(opy, opx).BackColor = Color.White
        emptyspace(opy, opx).avail = False
    End Sub

End Class
