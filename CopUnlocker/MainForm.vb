
Partial Public Class MainForm

    Dim flag1, flag2, flag3, flag4 As Boolean

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        flag1 = CheckBox1.Checked
        flag2 = CheckBox2.Checked
        flag3 = CheckBox3.Checked
        flag4 = CheckBox4.Checked
        Panel2.Visible = False
        Panel3.Visible = True
        Panel3.Dock = DockStyle.Fill
        Me.Refresh()

        ListBox1.Items.Add("正在修复...")
        Me.Refresh()
        Threading.Thread.Sleep(200)
        Try
            If flag1 = True Then
                ListBox1.Items.Add("正在破解电子教室程序...")
                Me.Refresh()
                Threading.Thread.Sleep(400)
                Dim p As Process()
                p = Process.GetProcessesByName("studentmain")
                For i = 0 To p.Length - 1
                    p(i).Kill()
                Next
                p = Process.GetProcessesByName("studentex")
                For i = 0 To p.Length - 1
                    p(i).Kill()
                Next
                p = Process.GetProcessesByName("student")
                For i = 0 To p.Length - 1
                    p(i).Kill()
                Next
                ListBox1.Items.Add("电子教室程序破解完毕")
            End If
            If flag2 = True Then
                ListBox1.Items.Add("正在修复Host...")
                Me.Refresh()
                Threading.Thread.Sleep(400)
                Dim hostbyte() As Byte = My.Resources.hosts
                FileIO.FileSystem.DeleteFile("C:\Windows\System32\drivers\etc\hosts")
                FileIO.FileSystem.WriteAllBytes("C:\Windows\System32\drivers\etc\hosts", hostbyte, False)
                ListBox1.Items.Add("Host修复完毕")
            End If
            If flag3 = True Then
                ListBox1.Items.Add("正在修复DNS...")
                Me.Refresh()
                Threading.Thread.Sleep(400)
                ResetDNS("8.8.8.8")
                ListBox1.Items.Add("DNS修复完毕")
            End If
            If flag4 = True Then
                ListBox1.Items.Add("正在修复策略组...")
                Me.Refresh()
                Threading.Thread.Sleep(400)
                'FileIO.FileSystem.DeleteDirectory("C:\Windows\System32\GroupPolicy", FileIO.DeleteDirectoryOption.DeleteAllContents)
                ' FileIO.FileSystem.CreateDirectory("C:\Windows\System32\GroupPolicy")



                FileIO.FileSystem.WriteAllText("xf.bat", My.Resources.xf, False)

                Shell("xf.bat", AppWinStyle.Hide, True)
                Dim p As Process()
                p = Process.GetProcessesByName("mmc")
                For i = 0 To p.Length - 1
                    p(i).Kill()
                Next
                FileIO.FileSystem.DeleteFile("xf.bat")
                ListBox1.Items.Add("策略组修复完毕")
            End If
            ListBox1.Items.Add("请等待修复完成...")

        Catch ex As Exception
            ListBox1.Items.Add(ex.Message)
            MsgBox(ex.Message)
        End Try

        Threading.Thread.Sleep(2000)
        Me.Refresh()
        Panel3.Visible = False
        Panel4.Visible = True
        Panel4.Dock = DockStyle.Fill
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ResetDNS(v As String, Optional w As String = "8.8.8.8")
        Dim inPar As Management.ManagementBaseObject = Nothing
        Dim outPar As Management.ManagementBaseObject = Nothing
        Dim mc As New Management.ManagementClass("Win32_NetworkAdapterConfiguration")
        Dim moc As Management.ManagementObjectCollection = mc.GetInstances()
        For Each mo As Management.ManagementObject In moc
            If Not CBool(mo("IPEnabled")) Then
                Continue For
            End If

            inPar = mo.GetMethodParameters("SetDNSServerSearchOrder")
            Dim dns1 As String = v
            Dim dns2 As String = w
            inPar("DNSServerSearchOrder") = New String() {dns1, dns2}
            outPar = mo.InvokeMethod("SetDNSServerSearchOrder", inPar, Nothing)
            Exit For
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Panel1.Visible = False
        Panel2.Visible = True
        Panel2.Dock = DockStyle.Fill
    End Sub

    Private Sub MainForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        InitializeComponent()
        Panel1.Visible = True
        Panel1.Dock = DockStyle.Fill
    End Sub
End Class
