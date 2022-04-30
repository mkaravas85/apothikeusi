Public Class Form2
    Public formcontrol As String
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ShowIcon = False
        DataGridView1.Columns(0).Visible = False
        DataGridView1.Columns(1).HeaderText = "Κωδικός"
        DataGridView1.Columns(2).Width = 315
        If formcontrol = "pelprom" Then
            DataGridView1.Columns(2).HeaderText = "Επωνυμία"
        ElseIf formcontrol = "eidos" Then
            DataGridView1.Columns(2).HeaderText = "Περιγραφή"
        End If
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If formcontrol = "pelprom" Then
            pelprom = DataGridView1.CurrentRow.Cells(0).Value
            Form1.TextBox1.Text = DataGridView1.CurrentRow.Cells(2).Value.ToString
        ElseIf formcontrol = "eidos" Then
            eidos = DataGridView1.CurrentRow.Cells(0).Value
            Form1.TextBox2.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString

        End If
        Me.Close()

    End Sub
End Class