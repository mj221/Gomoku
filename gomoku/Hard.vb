Public Class HardMode
    Public Shared Data(50) As String    'Just a random number of arrays to store Match History and data
    Public Shared DataNum As Integer    'Used for storing number of arrays

    Private Structure Placement
        Public avail As Boolean         'Is used for eliminating occupied space on board

    End Structure

    Dim timer As Integer = 0            'timer to count time elapsed 
    Dim placementlimit = 0              'Value that counts how many stones are placed
    Dim possiblepr As Integer           'Value that will be used to determine what 'normal' moves the AI will take and how it will 'defend'
    Dim offencepr As Integer            'Value to be used to determine how the AI will attack
    Dim Board(19, 19) As Button         'Used to make a 19x19 board
    Dim opx, opy As Integer             'Value to store the opponents latest move (x,y)
    Dim altx2, alty2 As Integer         'Value used to store the player's latest move (x,y)
    Dim count As Integer = 0            'Value used in Ai2() to determine which normal/defence moves the AI will take
    Dim count2 As Integer = 0           'value used in Ai3() to determine which offence pattern the AI will take
    Dim count3 As Integer = 0           'value used in Computer() to switch the inital Ai2 to Ai3
    Dim CheckResult As Boolean = False      'To check for victory condition
    Dim UserWin As Boolean                   'Checks which side one
    Dim emptyspace(20, 20) As Placement      'alternative board/array to check for unavailable placements


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        tmrGame.Interval = 1000         'Set up timer: 1 second interval
        tmrGame.Start()
        AddHandler tmrGame.Tick, AddressOf tmrGame_Tick
        creategame()                    'creates game
        lblUsername.Text = "Player " & SingleMenu.username & " vs. Computer (Hard)"         'Displays the player's username and opponent
    End Sub


    Public Sub creategame()             'create game

        For i = 0 To 20                         'create an imaginary 21 by 21 board and make them all 'occupied'
            For j = 0 To 20
                emptyspace(i, j).avail = False
            Next
        Next
        For i = 1 To 19                         'create a real 19 by 19 board
            For j = 1 To 19
                Board(i, j) = New Button
                emptyspace(i, j).avail = True       'out of 21x21 occupied units, make 19x19 units 'unoccupied'
                Board(i, j).Text = ""
                Board(i, j).Name = i & ":" & j      'label each array by their placement in the array, separated by a semi colon
                Board(i, j).BackColor = Color.RosyBrown             'additional design elements
                Board(i, j).FlatStyle = FlatStyle.Flat
                Board(i, j).FlatAppearance.BorderColor = Color.Black
                Board(i, j).Top = 27 + (20 * i)
                Board(i, j).Left = 50 + (20 * j)
                Board(i, j).Width = 20
                Board(i, j).Height = 20
                Controls.Add(Board(i, j))

                AddHandler Board(i, j).Click, AddressOf board_click         'algorithm links to board_click
            Next
        Next

    End Sub
    Private Sub board_click(sender As Object, ByVal e As EventArgs)
        My.Computer.Audio.Play("C:\Users\MJL\Desktop\GOMOKU\gomoku\gomoku\bin\Debug\Stone.wav")         'sound effect when placing a stone
        sender.Text = "X"           'the player's placement is initally marked X
        sender.enabled = False      'disable that placement permanently 
        btnUndo.Enabled = True      'since now the user has placed a stone, he is able to 'Undo' his actions
        btnRedo.Enabled = True      'the user can now 'redo' his placement 
        sender.BackColor = Color.Black      'Fill the block black
        Dim alternate As String() = sender.name.Split(":")          'the coordinates of the placement made by the player can now be split up;  the stored i and j value as divided by ":"
        Dim alty = alternate(0)             'the i value is stored in alty
        Dim altx = alternate(1)             'the j value is stored in altx

        alty2 = alty                        'make alty2 equal to alty to share the value outside this routine
        altx2 = altx                        'make altx2 equal to altx to share the value outside this routine
        emptyspace(alty2, altx2).avail = False      'the placement made my the user is now "occupied"
        checkwin()                                  'check victory condition
        If CheckResult = False Then                    'if not then
            Call Computer()                            'opponent turn
        ElseIf CheckResult = True Then                  'if yes
            tmrGame.Enabled = False                     'stop timer
            Dim result = MessageBox.Show("Click 'Yes' to Main Menu or 'No' to Continue Playing", "Are you sure?", MessageBoxButtons.YesNo)
            'Asks whether the user now wants to leave
            If result = DialogResult.Yes Then           'if yes
                tmrGame.Enabled = False                 'timer must be stopped
                If UserWin = True Then                  'if User won, then send the line below to the Match History Form via using shared variables
                    Data(DataNum) = SingleMenu.username & " vs. " & "Computer (Hard)" & "  |  " & "Win " & "Timer: " & timer & " seconds"
                    MatchHistory.ModeNum = 3                'ModeNum determines which gamemode the above data is coming from. I did this in order to avoid crashes when variables are shared amongst other gamemodes.
                    MatchHistory.MatchAvail = True          'These two values will be sent to the MatchHistory form
                ElseIf UserWin = False Then             'if user didn't win, then send the line below to the Match History Form using the shared variable
                    Data(DataNum) = SingleMenu.username & " vs. " & "Computer (Hard)" & "  |  " & "Lose " & "Timer: " & timer & " seconds"
                    MatchHistory.ModeNum = 3
                    MatchHistory.MatchAvail = True
                End If
                MainMenu.Show()
                Me.Close()
            ElseIf result = DialogResult.No Then                    'if the user wants to stay in game (like in case they lost the game
                MessageBox.Show("The latest move made will be undone")
                Board(alty2, altx2).Enabled = True
                Board(alty2, altx2).Text = ""                       'the very latest move made by the player and the opponent will be removed/undone.
                Board(alty2, altx2).BackColor = Color.RosyBrown     'so that they can resume playing
                Board(opy, opx).Enabled = True
                Board(opy, opx).BackColor = Color.RosyBrown
                Board(opy, opx).Text = ""
                tmrGame.Enabled = True
                CheckResult = False
            End If

        End If


    End Sub

    Public Sub Computer()           'the computer will play a turn in 'Normal' AI (makes standard random moves) and then it will switch into an 'Offensive' AI

        If count3 = 0 Then
            Call Ai2()
            count3 = 1
        Else
            Call Ai3()
        End If
    End Sub


    Private Sub Ai2() 'regular placement/occasional defence
        If count = 0 Then
            Randomize()
            possiblepr = Int(Rnd() * 9 + 1)

        End If
        Select Case possiblepr                      'There are 8 cases in which the AI can place their stone. Most occasionally it would place a stone near 
                                                       'and accordingly to the user's placement as shown below
            Case 1  'west
                If emptyspace(alty2, altx2 - 1).avail = False Then          'if the space is already occupied randomize the case again
                    count = 0
                    Call Ai2()
                ElseIf Board(alty2, altx2 - 1).Text = "X" And Board(alty2, altx2 - 2).Text = "X" And emptyspace(alty2, altx2 - 3).avail = True Then         'this is the defense AI, it will randomly activate by chance when the user has 3 stones in a row
                    emptyspace(alty2, altx2 - 3).avail = False
                    Board(alty2, altx2 - 3).Text = " "              'blank is opponent's placement
                    Board(alty2, altx2 - 3).Enabled = False
                    Board(alty2, altx2 - 3).BackColor = Color.White
                    opy = alty2
                    opx = altx2 - 3
                    checkwin()
                ElseIf Board(alty2, altx2 - 1).Text = "" Then
                    Board(alty2, altx2 - 1).Text = " "                  'regular placement algorithm
                    Board(alty2, altx2 - 1).Enabled = False
                    Board(alty2, altx2 - 1).BackColor = Color.White
                    opy = alty2
                    opx = altx2 - 1
                    emptyspace(alty2, altx2 - 1).avail = False
                    checkwin()
                End If
            Case 2  'north west                                                         'repeat
                If emptyspace(alty2 - 1, altx2 - 1).avail = False Then
                    count = 0
                    Call Ai2()
                ElseIf Board(alty2 - 1, altx2 - 1).Text = "X" And Board(alty2 - 2, altx2 - 2).Text = "X" And emptyspace(alty2 - 3, altx2 - 3).avail = True Then
                    emptyspace(alty2 - 3, altx2 - 3).avail = False
                    Board(alty2 - 3, altx2 - 3).Text = " "
                    Board(alty2 - 3, altx2 - 3).BackColor = Color.White
                    Board(alty2 - 3, altx2 - 3).Enabled = False
                    opy = alty2 - 3
                    opx = altx2 - 3
                    checkwin()
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
                    Call Ai2()
                ElseIf Board(alty2 - 1, altx2).Text = "X" And Board(alty2 - 2, altx2).Text = "X" And emptyspace(alty2 - 3, altx2).avail = True Then
                    emptyspace(alty2 - 3, altx2).avail = False
                    Board(alty2 - 3, altx2).Text = " "
                    Board(alty2 - 3, altx2).Enabled = False
                    Board(alty2 - 3, altx2).BackColor = Color.White
                    opy = alty2 - 3
                    opx = altx2
                    checkwin()
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
                    Call Ai2()
                ElseIf Board(alty2 - 1, altx2 + 1).Text = "X" And Board(alty2 - 2, altx2 + 2).Text = "X" And emptyspace(alty2 - 3, altx2 + 3).avail = True Then
                    Board(alty2 - 3, altx2).Text = " "
                    Board(alty2 - 3, altx2).Enabled = False
                    Board(alty2 - 3, altx2).BackColor = Color.White
                    opy = alty2 - 3
                    opx = altx2 + 3
                    emptyspace(alty2 - 3, altx2 + 3).avail = False
                    checkwin()
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
                    Call Ai2()
                ElseIf Board(alty2, altx2 + 1).Text = "X" And Board(alty2, altx2 + 2).Text = "X" And emptyspace(alty2, altx2 + 3).avail = True Then
                    Board(alty2, altx2 + 3).Text = " "
                    Board(alty2, altx2 + 3).Enabled = False
                    Board(alty2, altx2 + 3).BackColor = Color.White
                    opy = alty2
                    opx = altx2 + 3
                    emptyspace(alty2, altx2 + 3).avail = False
                    checkwin()
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
                    Call Ai2()
                ElseIf Board(alty2 + 1, altx2 + 1).Text = "X" And Board(alty2 + 2, altx2 + 2).Text = "X" And emptyspace(alty2 + 3, altx2 + 3).avail = True Then
                    Board(alty2 + 3, altx2 + 3).Text = " "
                    Board(alty2 + 3, altx2 + 3).Enabled = False
                    Board(alty2 + 3, altx2 + 3).BackColor = Color.White
                    opy = alty2 + 3
                    opx = altx2 + 3
                    emptyspace(alty2 + 3, altx2 + 3).avail = False
                    checkwin()
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
                    Call Ai2()
                ElseIf Board(alty2 + 1, altx2).Text = "X" And Board(alty2 + 2, altx2).Text = "X" And emptyspace(alty2 + 3, altx2).avail = True Then
                    Board(alty2 + 3, altx2).Text = " "
                    Board(alty2 + 3, altx2).Enabled = False
                    Board(alty2 + 3, altx2).BackColor = Color.White
                    opy = alty2 + 3
                    opx = altx2 + 3
                    emptyspace(alty2 + 3, altx2).avail = False
                    checkwin()
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
                    Call Ai2()
                ElseIf Board(alty2 + 1, altx2 - 1).Text = "X" And Board(alty2 + 2, altx2 - 2).Text = "X" And emptyspace(alty2 + 3, altx2 - 3).avail = True Then
                    Board(alty2 + 3, altx2 - 3).Text = " "
                    Board(alty2 + 3, altx2 - 3).Enabled = False
                    Board(alty2 + 3, altx2 - 3).BackColor = Color.White
                    opy = alty2 + 3
                    opx = altx2 - 3
                    emptyspace(alty2 + 3, altx2 - 3).avail = False
                    checkwin()
                ElseIf Board(alty2 + 1, altx2 - 1).Text = "" Then
                    Board(alty2 + 1, altx2 - 1).Text = " "
                    Board(alty2 + 1, altx2 - 1).Enabled = False
                    Board(alty2 + 1, altx2 - 1).BackColor = Color.White
                    opy = alty2 + 1
                    opx = altx2 - 1
                    emptyspace(alty2 + 1, altx2 - 1).avail = False
                    checkwin()
                End If
            Case 9  'if all possible placements are locked/occupied then (in order to avoid crash), the Ai1() will be called
                If emptyspace(alty2, altx2 - 1).avail = False And emptyspace(alty2 - 1, altx2 - 1).avail = False And emptyspace(alty2 - 1, altx2).avail = False And emptyspace(alty2 - 1, altx2 + 1).avail = False And emptyspace(alty2, altx2 + 1).avail = False And emptyspace(alty2 + 1, altx2 + 1).avail = False And emptyspace(alty2 + 1, altx2).avail = False And emptyspace(alty2 + 1, altx2 - 1).avail = False Then
                    Call Ai1()
                Else
                    count = 0
                    Call Ai2()
                End If

        End Select

    End Sub

    Private Sub Ai1()   'Randomized Placement AI
        Dim row, col As Integer

        Randomize()                           'Places at random
        row = Int(Rnd() * 19 + 1)
        col = Int(Rnd() * 19 + 1)
        While Board(row, col).Text = "X" Or Board(row, col).Text = " "
            row = Int(Rnd() * 19 + 1)
            col = Int(Rnd() * 19 + 1)
        End While
        Board(row, col).Text = " "
        Board(row, col).Enabled = False
        Board(row, col).BackColor = Color.White
        opy = row
        opx = col
        checkwin()
    End Sub



    Private Sub Ai3() 'offence

        If count2 = 0 Then
            Randomize()
            offencepr = Int(Rnd() * 8 + 1)

        End If

        Select Case offencepr
            Case 1 'west
                If emptyspace(opy, opx - 1).avail = False Then      'If the unit of displaced range is occupied then call Ai2()
                    Call Ai2()
                    count2 = 0

                ElseIf Board(opy, opx - 1).Text = "X" Then          're-check for opponent's placement (this maybe required in case the opponent decides to cut off the AI's streak of stones
                    count2 = 0
                    Call Ai3()
                ElseIf Board(opy, opx - 1).Text = " " Then          'check again and if all clear replace current x,y coordinates by the available coordinates
                    count2 = 1

                    opy = opy
                    opx = opx - 1

                    Call Ai3()
                Else
                    Board(opy, opx - 1).Text = " "                      'if all clear, keeping placing stones in the same direction
                    Board(opy, opx - 1).Enabled = False
                    Board(opy, opx - 1).BackColor = Color.White
                    opy = opy
                    opx = opx - 1
                    count2 = 1
                    checkwin()                                          'checkwin 
                End If
            Case 2  'north west                                                         'repeat
                If emptyspace(opy - 1, opx - 1).avail = False Then
                    Call Ai2()
                    count2 = 0

                ElseIf Board(opy - 1, opx - 1).Text = "X" Then
                    count2 = 0
                    Call Ai3()

                ElseIf Board(opy - 1, opx - 1).Text = " " Then
                    count2 = 1
                    opy = opy - 1
                    opx = opx - 1
                    Call Ai3()
                Else

                    Board(opy - 1, opx - 1).Text = " "
                    Board(opy - 1, opx - 1).Enabled = False
                    Board(opy - 1, opx - 1).BackColor = Color.White
                    opy = opy - 1
                    opx = opx - 1
                    count2 = 1
                    checkwin()
                End If
            Case 3 'north
                If emptyspace(opy - 1, opx).avail = False Then
                    Call Ai1()
                    count2 = 0

                ElseIf Board(opy - 1, opx).Text = "X" Then
                    count2 = 0
                    Call Ai3()

                ElseIf Board(opy - 1, opx).Text = " " Then
                    count2 = 1
                    opy = opy - 1
                    opx = opx
                    Call Ai3()
                Else
                    Board(opy - 1, opx).Text = " "
                    Board(opy - 1, opx).Enabled = False
                    Board(opy - 1, opx).BackColor = Color.White
                    opy = opy - 1
                    opx = opx
                    count2 = 1
                    checkwin()
                End If
            Case 4 'north east
                If emptyspace(opy - 1, opx + 1).avail = False Then
                    Call Ai1()
                    count2 = 0

                ElseIf Board(opy - 1, opx + 1).Text = "X" Then
                    count2 = 0
                    Call Ai3()

                ElseIf Board(opy - 1, opx + 1).Text = " " Then
                    count2 = 1
                    opy = opy - 1
                    opx = opx + 1
                    Call Ai3()
                Else
                    Board(opy - 1, opx + 1).Text = " "
                    Board(opy - 1, opx + 1).Enabled = False
                    Board(opy - 1, opx + 1).BackColor = Color.White
                    opy = opy - 1
                    opx = opx + 1
                    count2 = 1
                    checkwin()
                End If
            Case 5 'east
                If emptyspace(opy, opx + 1).avail = False Then
                    Call Ai1()
                    count2 = 0

                ElseIf Board(opy, opx + 1).Text = "X" Then
                    count2 = 0
                    Call Ai3()

                ElseIf Board(opy, opx + 1).Text = " " Then
                    count2 = 1
                    opy = opy
                    opx = opx + 1
                    Call Ai3()
                Else
                    Board(opy, opx + 1).Text = " "
                    Board(opy, opx + 1).Enabled = False
                    Board(opy, opx + 1).BackColor = Color.White
                    opy = opy
                    opx = opx + 1
                    count2 = 1
                    checkwin()
                End If
            Case 6 'south east
                If emptyspace(opy + 1, opx + 1).avail = False Then
                    Call Ai1()
                    count2 = 0

                ElseIf Board(opy + 1, opx + 1).Text = "X" Then
                    count2 = 0
                    Call Ai3()

                ElseIf Board(opy + 1, opx + 1).Text = " " Then
                    count2 = 1
                    opy = opy + 1
                    opx = opx + 1
                    Call Ai3()
                Else
                    Board(opy + 1, opx + 1).Text = " "
                    Board(opy + 1, opx + 1).Enabled = False
                    Board(opy + 1, opx + 1).BackColor = Color.White
                    opy = opy + 1
                    opx = opx + 1
                    count2 = 1
                    checkwin()
                End If
            Case 7 'south
                If emptyspace(opy + 1, opx).avail = False Then
                    Call Ai1()
                    count2 = 0
                ElseIf Board(opy + 1, opx).Text = "X" Then
                    count2 = 0
                    Call Ai3()

                ElseIf Board(opy + 1, opx).Text = " " Then
                    count2 = 1
                    opy = opy + 1
                    opx = opx
                    Call Ai3()
                Else
                    Board(opy + 1, opx).Text = " "
                    Board(opy + 1, opx).Enabled = False
                    Board(opy + 1, opx).BackColor = Color.White
                    opy = opy + 1
                    opx = opx
                    count2 = 1
                    checkwin()
                End If
            Case 8 'south west
                If emptyspace(opy + 1, opx - 1).avail = False Then
                    Call Ai1()
                    count2 = 0

                ElseIf Board(opy + 1, opx - 1).Text = "X" Then
                    count2 = 0
                    Call Ai3()

                ElseIf Board(opy + 1, opx - 1).Text = " " Then
                    count2 = 1
                    opy = opy + 1
                    opx = opx - 1
                    Call Ai3()
                Else
                    Board(opy + 1, opx - 1).Text = " "
                    Board(opy + 1, opx - 1).Enabled = False
                    Board(opy + 1, opx - 1).BackColor = Color.White
                    opy = opy + 1
                    opx = opx - 1
                    count2 = 1
                    checkwin()
                End If
        End Select
    End Sub

    Public Sub checkwin()
        placementlimit += 1             'add 1 unit of value to the variable
        For i = 1 To 15                     'The algorithm below checks for any combination of 5 stones in a line 
            For j = 1 To 15                 'if there is a line of 5 stones then it returns their corresponding boolean variables which is used at Line 77 and Line623
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
                    UserWin = True
                    MessageBox.Show("User " & SingleMenu.username & " Win!")
                    CheckResult = True
                End If
            Next
        Next
        For i = 1 To 19
            For j = 1 To 15
                If Board(i, j).Text = "X" And Board(i, j + 1).Text = "X" And Board(i, j + 2).Text = "X" And Board(i, j + 3).Text = "X" And Board(i, j + 4).Text = "X" Then
                    UserWin = True
                    MessageBox.Show("User " & SingleMenu.username & " Win!")
                    CheckResult = True
                End If
            Next
        Next
        For i = 1 To 15
            For j = 5 To 19
                If Board(i, j).Text = "X" And Board(i + 1, j - 1).Text = "X" And Board(i + 2, j - 2).Text = "X" And Board(i + 3, j - 3).Text = "X" And Board(i + 4, j - 4).Text = "X" Then
                    UserWin = True
                    MessageBox.Show("User " & SingleMenu.username & " Win!")
                    CheckResult = True
                End If
            Next
        Next
        'for Opponent
        For i = 1 To 15
            For j = 1 To 15
                If Board(i, j).Text = " " And Board(i + 1, j + 1).Text = " " And Board(i + 2, j + 2).Text = " " And Board(i + 3, j + 3).Text = " " And Board(i + 4, j + 4).Text = " " Then
                    MessageBox.Show("Computer Wins!")
                    UserWin = False

                    CheckResult = True
                End If
            Next
        Next
        For i = 1 To 15
            For j = 1 To 19
                If Board(i, j).Text = " " And Board(i + 1, j).Text = " " And Board(i + 2, j).Text = " " And Board(i + 3, j).Text = " " And Board(i + 4, j).Text = " " Then
                    MessageBox.Show("Computer Wins!")
                    UserWin = False

                    CheckResult = True
                End If
            Next
        Next
        For i = 1 To 19
            For j = 1 To 15
                If Board(i, j).Text = " " And Board(i, j + 1).Text = " " And Board(i, j + 2).Text = " " And Board(i, j + 3).Text = " " And Board(i, j + 4).Text = " " Then
                    MessageBox.Show("Computer Wins!")
                    UserWin = False

                    CheckResult = True
                End If
            Next
        Next
        For i = 5 To 15
            For j = 5 To 19
                If Board(i, j).Text = " " And Board(i + 1, j - 1).Text = " " And Board(i + 2, j - 2).Text = " " And Board(i + 3, j - 3).Text = " " And Board(i + 4, j - 4).Text = " " Then
                    MessageBox.Show("Computer Wins!")
                    UserWin = False

                    CheckResult = True
                End If
            Next
        Next
        If placementlimit = 180 Then            'In a 19x19 board, 180 stones per each person are the maximum number of placements. If no victory is attained within the limit, the game will restart
            MessageBox.Show("You have reached the maximum number of placement available (In this situation, this match won't be listed in 'Match History'. You can restart the game after clicking OK to Confirm")
            For i = 0 To 19
                For j = 0 To 19
                    Board(i, j).Text = ""
                    Board(i, j).Enabled = True
                    Board(i, j).BackColor = Color.RosyBrown
                Next
            Next
            btnUndo.Enabled = False
            btnRedo.Enabled = False
            tmrGame.Enabled = True
            timer = 0
        End If
        If UserWin = False Then

            If CheckResult = True Then      'this is the same algorithm from Line 77. I had to put it here to reevaluate the victory condition for the AI opponent as it misses out on this event if the player wins, thus the program becomes bugged
                tmrGame.Enabled = False
                Dim result = MessageBox.Show("Click 'Yes' to Main Menu or 'No' to Continue Playing", "Are you sure?", MessageBoxButtons.YesNo)

                If result = DialogResult.Yes Then
                    tmrGame.Enabled = False
                    If UserWin = True Then
                        Data(DataNum) = SingleMenu.username & " vs. " & "Computer (Hard)" & "  |  " & "Win " & "Timer: " & timer & " seconds"
                        MatchHistory.ModeNum = 3
                        MatchHistory.MatchAvail = True
                    ElseIf UserWin = False Then
                        Data(DataNum) = SingleMenu.username & " vs. " & "Computer (Hard)" & "  |  " & "Lose " & "Timer: " & timer & " seconds"
                        MatchHistory.ModeNum = 3
                        MatchHistory.MatchAvail = True
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
        End If
    End Sub

    Private Sub tmrGame_Tick(sender As Object, e As EventArgs)      'Timer used in a game and is displayed in the Match History Form
        timer += 1
        lblTimer.Text = "Time Elapsed: " & timer & " seconds"
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs)   'Opens search library but cannot load or save files
        Dim saveFileDialog1 As New SaveFileDialog()
        saveFileDialog1.Filter = "XML File|*.xml|All Files|*."
        saveFileDialog1.Title = "Save Game State"
        saveFileDialog1.ShowDialog()
    End Sub

    Private Sub LoadToolStripMenuItem_Click(sender As Object, e As EventArgs)    'Opens search library but cannot load or save files
        Dim OpenFileDialog1 As New OpenFileDialog()
        OpenFileDialog1.Filter = "XML File|*.xml|All Files|*."
        OpenFileDialog1.Title = "Load Game State"
        OpenFileDialog1.ShowDialog()
    End Sub
    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        Help.Show()
    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click   'Confirms exit
        My.Computer.Audio.Play("C:\Users\MJL\Desktop\GOMOKU\gomoku\gomoku\bin\Debug\Button.wav")
        Dim result = MessageBox.Show(" Do you wish to go to the Game Menu?", "Confirm", MessageBoxButtons.YesNo)
        tmrGame.Enabled = False
        If result = DialogResult.Yes Then

            GameMode.Show()
            Me.Close()
        Else
            tmrGame.Enabled = True
        End If
    End Sub
    Private Sub btnMainMenu_Click(sender As Object, e As EventArgs) Handles btnMainMenu.Click       'Confirms exit
        My.Computer.Audio.Play("C:\Users\MJL\Desktop\GOMOKU\gomoku\gomoku\bin\Debug\Button.wav")
        Dim result = MessageBox.Show(" Do you wish to go to the Main Menu?", "Confirm", MessageBoxButtons.YesNo)
        tmrGame.Enabled = False
        If result = DialogResult.Yes Then

            MainMenu.Show()
            Me.Close()
        Else
            tmrGame.Enabled = True
        End If
    End Sub
    Private Sub GameMenuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GameMenuToolStripMenuItem.Click   'Confirms exit
        Dim result = MessageBox.Show(" Do you wish to go to the Game Menu?", "Confirm", MessageBoxButtons.YesNo)
        tmrGame.Enabled = False
        If result = DialogResult.Yes Then

            MainMenu.Show()
            Me.Close()
        Else
            tmrGame.Enabled = True
        End If
    End Sub

    Private Sub ResetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetToolStripMenuItem.Click
        CheckResult = False                     'Reset/clear exisitng values
        For i = 1 To 19
            For j = 1 To 19
                Board(i, j).Text = ""
                Board(i, j).Enabled = True
                Board(i, j).BackColor = Color.RosyBrown
                emptyspace(i, j).avail = True
            Next
        Next
        btnUndo.Enabled = False
        btnRedo.Enabled = False
        tmrGame.Enabled = True
        timer = 0



    End Sub
    Private Sub MainMenuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MainMenuToolStripMenuItem.Click   'confirm exit
        Dim result = MessageBox.Show(" Do you wish to go to the Main Menu?", "Confirm", MessageBoxButtons.YesNo)
        tmrGame.Enabled = False
        If result = DialogResult.Yes Then

            MainMenu.Show()
            Me.Close()
        Else
            tmrGame.Enabled = True
        End If
    End Sub

    Private Sub btnUndo_Click(sender As Object, e As EventArgs) Handles btnUndo.Click           'Uses the temporary x,y values found eariler to Undo
        My.Computer.Audio.Play("C:\Users\MJL\Desktop\GOMOKU\gomoku\gomoku\bin\Debug\ting.wav")
        Board(alty2, altx2).Enabled = True
        Board(alty2, altx2).Text = ""
        Board(alty2, altx2).BackColor = Color.RosyBrown
        emptyspace(alty2, altx2).avail = True
        Board(opy, opx).Enabled = True
        Board(opy, opx).Text = ""
        Board(opy, opx).BackColor = Color.RosyBrown
        emptyspace(opy, opx).avail = True
    End Sub

    Private Sub UndoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UndoToolStripMenuItem.Click   'Uses the temporary x,y values found eariler to Undo
        Board(alty2, altx2).Enabled = True
        Board(alty2, altx2).Text = ""
        Board(alty2, altx2).BackColor = Color.RosyBrown
        emptyspace(alty2, altx2).avail = True
        Board(opy, opx).Enabled = True
        Board(opy, opx).Text = ""
        Board(opy, opx).BackColor = Color.RosyBrown
        emptyspace(opy, opx).avail = True
    End Sub
    Private Sub RedoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RedoToolStripMenuItem.Click   'Uses the temporary x,y values found eariler to Redo
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
        My.Computer.Audio.Play("C:\Users\MJL\Desktop\GOMOKU\gomoku\gomoku\bin\Debug\Background.wav")        'plays background music if clicked
    End Sub

    Private Sub btnRedo_Click(sender As Object, e As EventArgs) Handles btnRedo.Click   'Uses the temporary x,y values found eariler to Redo
        My.Computer.Audio.Play("C:\Users\MJL\Desktop\GOMOKU\gomoku\gomoku\bin\Debug\ting.wav")
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

