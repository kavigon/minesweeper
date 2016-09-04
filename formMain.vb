Public Class formMain

    Private rowNum As Integer = 10
    Private columNum As Integer = 10
    Private mineNum As Integer = 10

    Private btnArray(rowNum - 1, columNum - 1) As Button
    Private mineArray(rowNum - 1, columNum - 1) As Boolean

    Private Sub formMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'make 10*10 
        panelButton.Size = Me.ClientSize
        For i As Integer = 0 To rowNum - 1
            For j As Integer = 0 To columNum - 1
                btnArray(i, j) = New Button()
                btnArray(i, j).Name = "btn" & i.ToString & j.ToString
                btnArray(i, j).Size = New Size(35, 35)
                btnArray(i, j).Location = New Point(10 + (j * 35), 10 + (i * 35))
                btnArray(i, j).Tag = i & "," & j
                panelButton.Controls.Add(btnArray(i, j))
                AddHandler btnArray(i, j).Click, AddressOf btnArray_Click
            Next
        Next

        'set mine
        mineArray.Initialize()
        Dim idx As Integer = 0
        While idx < 100
            Dim rowIdx As Integer = genRandom(0, rowNum - 1)
            Dim colIdx As Integer = genRandom(0, columNum - 1)

            mineArray(rowIdx, colIdx) = True

            ' If index is 10, exit the loop.
            Dim mineIdx As Integer = 0
            For i As Integer = 0 To rowNum - 1
                For j As Integer = 0 To columNum - 1
                    If mineArray(i, j) = True Then
                        mineIdx += 1
                        If mineIdx = mineNum Then
                            Exit While
                        End If
                    End If
                Next
            Next
        End While
    End Sub

    'event handler when push the btn
    Public Sub btnArray_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim clickBtn As Button
        Dim row, colum As Integer

        clickBtn = sender

        row = Split(clickBtn.Tag, ",")(0)
        colum = Split(clickBtn.Tag, ",")(1)

        displayMine(row, colum)
    End Sub


    Public Function genRandom(ByVal Min As Integer, ByVal Max As Integer) As Integer
        Static generator As System.Random = New System.Random(System.DateTime.Now.Millisecond)
        Return generator.Next(Min, Max)
    End Function


    Dim xDirection As Integer() = {-1, +1, -1, -1, +1, +1, 0, 0}
    Dim yDirection As Integer() = {0, 0, -1, +1, -1, +1, -1, +1}

    Private Sub displayMine(row As Integer, colum As Integer)
        Dim countMine As Integer = 0

        If findMine(row, colum) = True Then
            btnArray(row, colum).Text = "M"
            btnArray(row, colum).Enabled = False
            MsgBox("BoooooooooooM")
        Else
            For i As Integer = 0 To 7
                If findMine(row + xDirection(i), colum + yDirection(i)) Then
                    countMine += 1
                End If
            Next

            If countMine = 0 Then
                btnArray(row, colum).Text = ""
            Else
                btnArray(row, colum).Text = countMine.ToString
            End If
            btnArray(row, colum).Enabled = False
        End If
    End Sub

    Private Function findMine(row As Integer, colum As Integer) As Boolean

        If row < 0 Or colum < 0 Or row >= rowNum Or colum >= columNum Then
            Return False
        Else
            Return mineArray(row, colum)
        End If

    End Function


    Private Sub Button1_Click(sender As Object, e As EventArgs)

        Dim minenum As Integer = 0
        For i As Integer = 0 To rowNum - 1
            For j As Integer = 0 To columNum - 1
                Debug.WriteLine(mineArray(i, j).ToString)
                If mineArray(i, j) = True Then
                    minenum += 1
                End If
            Next
        Next
        Debug.WriteLine(minenum.ToString)
    End Sub
End Class
