Public Class Form1
    Dim date1, date2 As String
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ShowIcon = False
    End Sub
    Private Sub frmCustomerDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox1.Focused Then
                TextBox2.Text = ""
                pelprom = 0
                eidos = 0
                dtvaluetoset("select pelprom,code,eponymia from pelproms where ptype='ΠΕΛΑΤΗΣ' and eponymia like '" & TextBox1.Text & "%'")

                If dt.Rows.Count = 1 Then
                    TextBox1.Text = dt.Rows(0).Item(2).ToString
                    pelprom = dt.Rows(0).Item(0)
                ElseIf dt.Rows.Count > 1 Then
                    Form2.formcontrol = "pelprom"
                    loadgrid("select pelprom,code,eponymia from pelproms where ptype='ΠΕΛΑΤΗΣ' and eponymia like '" & TextBox1.Text & "%'", Form2.DataGridView1)
                    Form2.Show()
                Else
                    MessageBox.Show("Δε βρέθηκαν πελάτες")
                End If
            End If
            If TextBox2.Focused Then
                dtvaluetoset("select eidos,code from eidi where code like '" & TextBox2.Text & "%' and pelprom= '" & pelprom & "%'")
                If dt.Rows.Count = 1 Then
                    TextBox2.Text = dt.Rows(0).Item(1).ToString
                    eidos = dt.Rows(0).Item(0)
                ElseIf dt.Rows.Count > 1 Then
                    Form2.formcontrol = "eidos"
                    loadgrid("select eidos,code,perigrafi from eidi where code like '" & TextBox2.Text & "%' and pelprom= '" & pelprom & "%'", Form2.DataGridView1)
                    Form2.Show()
                End If
            End If

        End If
    End Sub
    Private Sub gridlayout()
        'If eidos = 0 Then
        DataGridView1.Columns(0).HeaderText = "Κωδικός"
        DataGridView1.Columns(1).HeaderText = "Περιγραφή"
        DataGridView1.Columns(2).HeaderText = "Αρ. Παρτίδας"

        DataGridView1.Columns(3).HeaderText = "Ημ/νία Ολοκλήρωσης"

        DataGridView1.Columns(4).HeaderText = "Ποσότητα Παραγωγής"
        DataGridView1.Columns(5).HeaderText = "Ποσότητα Αποθήκευσης"
        DataGridView1.Columns(6).HeaderText = "Φύρα"
        DataGridView1.Columns(7).HeaderText = "Ποσοστό Φύρας (%)"
        DataGridView1.Columns(6).DefaultCellStyle.Format = "N0"
        'Else
        'DataGridView1.Columns(0).HeaderText = "Αρ. Παρτίδας"
        '    DataGridView1.Columns(1).HeaderText = "Ποσότητα Παραγωγής"
        '    DataGridView1.Columns(2).HeaderText = "Ποσότητα Αποθήκευσης"
        '    DataGridView1.Columns(3).HeaderText = "Φύρα"
        '    DataGridView1.Columns(4).HeaderText = "Ποσοστό Φύρας (%)"
        '    DataGridView1.Columns(1).DefaultCellStyle.Format = "N0"
        'End If
        DataGridView1.Columns(4).DefaultCellStyle.Format = "N0"
        DataGridView1.Columns(5).DefaultCellStyle.Format = "N0"
        DataGridView1.Columns(1).Width = 350
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TODATE(DateTimePicker1, date1)
        TODATE(DateTimePicker2, date2)

        If pelprom = 0 Then
            MessageBox.Show("Δεν έχετε επιλέξει πελάτη")
            Exit Sub
        End If
        If eidos = 0 Then
            'loadgrid("select a.eidos1,quantity-posotita2 from(select eidi.eidos,eidi.code as eidos1,sum(endqty) as quantity from paragogi join partides on paragogi.partida=partides.partidaid join eidi on eidi.eidos=partides.eidos where eidi.eidos in (select eidi.eidos from eidi where pelprom='" & pelprom & "') and endqty is not null group by eidi.eidos) a join (select eidos,sum(posotita) as posotita2 from grammeskin where grammeskin.kinisi in (select kiniseis.kinisi from kiniseis where palletanumber is not null) and  eidos in (select eidos from eidi where pelprom='" & pelprom & "') and axia is null group by eidos) b on a.eidos=b.eidos group by a.eidos", DataGridView1)
            loadgrid("select a.kwdikos,a.perigrafi,a.partidanumber,a.datecompleted,posotita1,posotita2,posotita1-posotita2,round(((posotita1-posotita2)/posotita1)*100,2) from (select eidi.code as kwdikos,eidi.perigrafi as perigrafi,endqty as posotita1,partidanumber,datecompleted from paragogi  join partides on paragogi.partida=partides.partidaid join eidi on eidi.eidos=partides.eidos where eidi.eidos in (select eidi.eidos from eidi where pelprom='" & pelprom & "') and endqty is not null and datecompleted>='" & date1 & "' and datecompleted<='" & date2 & "') a join (select partida,sum(posotita) as posotita2 from grammeskin join kiniseis on grammeskin.kinisi= kiniseis.kinisi where palletanumber is not null and  eidos in (select eidi.eidos from eidi where pelprom='" & pelprom & "') and axia is null group by kiniseis.partida) b on a.partidanumber=b.partida", DataGridView1)

        Else
            loadgrid("select a.kwdikos,a.perigrafi,a.partidanumber,a.datecompleted,posotita1,posotita2,posotita1-posotita2,round(((posotita1-posotita2)/posotita1)*100,2) from (select eidi.code as kwdikos,eidi.perigrafi as perigrafi,endqty as posotita1,partidanumber,datecompleted from paragogi  join partides on paragogi.partida=partides.partidaid join eidi on eidi.eidos=partides.eidos where eidi.eidos='" & eidos & "' and endqty is not null and datecompleted>='" & date1 & "' and datecompleted<='" & date2 & "') a join (select partida,sum(posotita) as posotita2 from grammeskin join kiniseis on grammeskin.kinisi= kiniseis.kinisi where palletanumber is not null  and  eidos='" & eidos & "' and axia is null group by kiniseis.partida) b on a.partidanumber=b.partida", DataGridView1)
            'select a.partidanumber,posotita1-posotita2 from (select endqty as posotita1,partidanumber from paragogi  join partides on paragogi.partida=partides.partidaid join eidi on eidi.eidos=partides.eidos where eidi.eidos=10485 and endqty is not null) a join (select partida,sum(posotita) as posotita2 from grammeskin join kiniseis on grammeskin.kinisi= kiniseis.kinisi where palletanumber is not null  and  eidos=10485 and axia is null group by kiniseis.partida) b on a.partidanumber=b.partida
        End If
        gridlayout()
    End Sub
End Class
