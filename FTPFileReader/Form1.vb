Imports System.IO
Imports System.Net
Imports WinSCP
Imports FTPFileReader.IniFile
Imports System.Threading

Public Class Form1
    Private FTPFileFolder As String
    Private MonthToTake As String = ""
    Private YearToTake As String = ""
    'Private DateToTake As Date
    Private FolderBrowserDialog1 As FolderBrowserDialog = New FolderBrowserDialog
    Private synchronizationContext As SynchronizationContext
    Private lstUsers As List(Of String) = New List(Of String)
    Private MonthLineNos As List(Of Integer) = New List(Of Integer)
    Private TallyInfo As Dictionary(Of String, UserInfo) = New Dictionary(Of String, UserInfo)

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        lblTotal.Text = ""
        MonthLineNos = New List(Of Integer)
        Dim CurMonthToTake As String = ""
        Dim CurYearToTake As String = ""
        MonthToTake = ""
        YearToTake = ""
        If Me.txtFolder.Text.Trim = "" Then
            MessageBox.Show("Please browse to the FTP File Folder.")
            Return
        End If
        Dim DirList As New ArrayList
        Dim Dirs() As String
        Try
            Dirs = Directory.GetDirectories(FTPFileFolder)
        Catch ex As Exception
            MessageBox.Show("Folder " & FTPFileFolder & " not found. Please browse to the FTP File Folder.")
            Return
        End Try
        RichTextBox2.Clear()
        RichTextBox3.Clear()
        TallyInfo = New Dictionary(Of String, UserInfo)
        DirList.AddRange(Dirs)
        DirList.Sort()
        Dim CurrentUser As String = ""
        Me.lstUsers = New List(Of String)
        Dim Infos As List(Of UserInfo) = New List(Of UserInfo)
        For Each Dir As String In Dirs
            ' Dir="...\FTP\AboveAllGyprockPlastering_14-10-2021"
            CurrentUser = Path.GetFileName(Dir) 'AboveAllGyprockPlastering_14-10-2021
            If CurrentUser.ToUpper.Contains("CANCEL") Then
                Continue For
            End If
            If Not (lstUsers.Contains(CurrentUser)) Then
                lstUsers.Add(CurrentUser)
            End If
            Dim JpgOpened As Integer = 0
            Dim PdfOpened As Integer = 0
            Dim Files = Directory.GetFiles(Dir) '"c:\")
            For Each sFile As String In Files 'Only One for each folder
                'sFile=".....\FTP\AboveAllGyprockPlastering_14-10-2021\AboveAllGyprockPlastering_14-10-2021.txt"
                Dim fname As String = Path.GetFileName(sFile).ToLower
                If Not (fname.Contains("_")) OrElse Not (fname.Contains(".txt")) Then
                    Continue For
                End If
                Dim Lines As String() = File.ReadAllLines(sFile)
                For Each Line As String In Lines
                    If Line.StartsWith("jpg opened") Then
                        'jpg opened October:  2020 ----0  <---Last Number
                        MonthToTake = Line.Split(":")(0).Replace("jpg opened", "").Trim '=October
                        'MonthToTake = MonthToTake.Substring(0, 3).ToUpper
                        YearToTake = Line.Split(":")(1).Trim.Substring(0, 4)
                        JpgOpened = CInt(Line.Substring(Line.IndexOf(YearToTake) + 5).Trim.Replace("-", ""))

                    ElseIf Line.StartsWith("pdf opened " & MonthToTake) AndAlso Line.Split(":")(1).Trim.StartsWith(YearToTake) Then
                        PdfOpened = CInt(Line.Substring(Line.IndexOf(YearToTake) + 5).Trim.Replace("-", ""))
                        Infos.Add(New UserInfo With {.UserName = CurrentUser, .MonthToTake = MonthToTake, .YearToTake = YearToTake, .JpgOpened = JpgOpened, .PdfOpened = PdfOpened})
                        CurMonthToTake = MonthToTake
                        CurYearToTake = YearToTake
                    End If
                Next 'Line

            Next 'File

        Next 'Folder
        If Infos.Count > 0 Then
            Dim res As New Dictionary(Of String, UserInfo)
            For Each Ui As UserInfo In Infos
                If res.ContainsKey(Ui.UserName & "|" & Ui.MonthToTake & "|" & Ui.YearToTake) Then
                    res(Ui.UserName & "|" & Ui.MonthToTake & "|" & Ui.YearToTake) = New UserInfo With {.JpgOpened = Ui.JpgOpened, .PdfOpened = Ui.PdfOpened}
                Else
                    res.Add(Ui.UserName & "|" & Ui.MonthToTake & "|" & Ui.YearToTake, New UserInfo With {.JpgOpened = Ui.JpgOpened, .PdfOpened = Ui.PdfOpened})
                End If

            Next
            For Each usr As String In lstUsers
                'UpdateUI2(usr, FormatUser:=True)
                UpdateUI2(usr)
                'Dim StartPos0 As List(Of Integer) = New List(Of Integer)
                'StartPos0.Add(0)
                UpdateUI2("JAN JPG: _JJAN, PDF: _PJAN" & vbTab & "APR JPG: _JAPR, PDF: _PAPR" & vbTab & "JUL JPG: _JJUL, PDF: _PJUL" & vbTab & "OCT JPG: _JOCT, PDF: _POCT" & vbTab) ', False, StartPos0)
                UpdateUI2("FEB JPG: _JFEB, PDF: _PFEB" & vbTab & "MAY JPG: _JMAY, PDF: _PMAY" & vbTab & "AUG JPG: _JAUG, PDF: _PAUG" & vbTab & "NOV JPG: _JNOV, PDF: _PNOV" & vbTab)
                UpdateUI2("MAR JPG: _JMAR, PDF: _PMAR" & vbTab & "JUN JPG: _JJUN, PDF: _PJUN" & vbTab & "SEP JPG: _JSEP, PDF: _PSEP" & vbTab & "DEC JPG: _JDEC, PDF: _PDEC" & vbTab)
                Dim MonthLine As String = ""
                Dim StartPos As List(Of Integer) = New List(Of Integer)
                Dim CurPos As Integer = 0
                Dim MonthTallied As List(Of String) = New List(Of String)
                StartPos.Add(CurPos)
                For Each key As String In res.Keys
                    Dim Ui2 As UserInfo = res(key)
                    If key.Split("|")(0) = usr Then
                        'MONTH 
                        Dim Mon As String = key.Split("|")(1).Substring(0, 3).ToUpper
                        MonthLine &= Mon & " " & "JPG: " & Ui2.JpgOpened & ", PDF: " & Ui2.PdfOpened & vbTab
                        'Me.RichTextBox2.Text = Me.RichTextBox2.Text.Replace("_J" & Mon, Ui2.JpgOpened).Replace("_P" & Mon, Ui2.PdfOpened)
                        UpdateUI2Replace("_J" & Mon, Ui2.JpgOpened)
                        UpdateUI2Replace("_P" & Mon, Ui2.PdfOpened)
                        CurPos = MonthLine.Length
                        StartPos.Add(CurPos)
                    End If
                Next
                'If MonthLine <> "" Then
                'UpdateUI2(MonthLine, False, StartPos)
                For k = 1 To 12
                    Dim PrevMon As String = CDate("2020-" & k.ToString.PadLeft(2, "0") & "-01").ToString("MMM").ToUpper
                    'Me.RichTextBox2.Text = Me.RichTextBox2.Text.Replace("_J" & PrevMon, "0").Replace("_P" & PrevMon, "0")
                    UpdateUI2Replace("_J" & PrevMon, "0")
                    UpdateUI2Replace("_P" & PrevMon, "0")
                Next
                'End If
            Next
            UpdateUI2Format("")
            'Tally
            For Each key As String In res.Keys
                Dim rMonthToTake As String = key.Split("|")(1).Substring(0, 3).ToUpper
                If TallyInfo.ContainsKey(rMonthToTake) Then
                    'TallyInfo(rMonthToTake) = New UserInfo With {.JpgOpened = res(key).JpgOpened, .PdfOpened = res(key).PdfOpened}
                    TallyInfo(rMonthToTake).JpgOpened = TallyInfo(rMonthToTake).JpgOpened + res(key).JpgOpened
                    TallyInfo(rMonthToTake).PdfOpened = TallyInfo(rMonthToTake).PdfOpened + res(key).PdfOpened
                Else
                    TallyInfo.Add(rMonthToTake, New UserInfo With {.JpgOpened = res(key).JpgOpened, .PdfOpened = res(key).PdfOpened})
                End If
            Next

            UpdateUI3("JAN JPG: _JJAN, PDF: _PJAN" & vbTab & "APR JPG: _JAPR, PDF: _PAPR" & vbTab & "JUL JPG: _JJUL, PDF: _PJUL" & vbTab & "OCT JPG: _JOCT, PDF: _POCT" & vbTab & vbNewLine &
            "FEB JPG: _JFEB, PDF: _PFEB" & vbTab & "MAY JPG: _JMAY, PDF: _PMAY" & vbTab & "AUG JPG: _JAUG, PDF: _PAUG" & vbTab & "NOV JPG: _JNOV, PDF: _PNOV" & vbTab & vbNewLine &
            "MAR JPG: _JMAR, PDF: _PMAR" & vbTab & "JUN JPG: _JJUN, PDF: _PJUN" & vbTab & "SEP JPG: _JSEP, PDF: _PSEP" & vbTab & "DEC JPG: _JDEC, PDF: _PDEC" & vbTab)

        Else
            UpdateUI("No record found.")
        End If
    End Sub
    Private Sub btnStart_ClickLUUGOOD(sender As Object, e As EventArgs) 'Handles btnStart.Click
        Dim CurMonthToTake As String = ""
        Dim CurYearToTake As String = ""
        MonthToTake = ""
        YearToTake = ""
        If Me.txtFolder.Text.Trim = "" Then
            MessageBox.Show("Please browse to the FTP File Folder.")
            Return
        End If
        Dim DirList As New ArrayList
        Dim Dirs() As String
        Try
            Dirs = Directory.GetDirectories(FTPFileFolder)
        Catch ex As Exception
            MessageBox.Show("Folder " & FTPFileFolder & " not found. Please browse to the FTP File Folder.")
            Return
        End Try
        RichTextBox2.Clear()
        DirList.AddRange(Dirs)
        DirList.Sort()
        Dim CurrentUser As String = ""
        Me.lstUsers = New List(Of String)
        Dim Infos As List(Of UserInfo) = New List(Of UserInfo)
        For Each Dir As String In Dirs
            ' Dir="...\FTP\AboveAllGyprockPlastering_14-10-2021"
            CurrentUser = Path.GetFileName(Dir) 'AboveAllGyprockPlastering_14-10-2021
            If CurrentUser.ToUpper.Contains("CANCEL") Then
                Continue For
            End If
            If Not (lstUsers.Contains(CurrentUser)) Then
                lstUsers.Add(CurrentUser)
            End If
            Dim JpgOpened As Integer = 0
            Dim PdfOpened As Integer = 0
            Dim Files = Directory.GetFiles(Dir) '"c:\")
            For Each sFile As String In Files 'Only One for each folder
                'sFile=".....\FTP\AboveAllGyprockPlastering_14-10-2021\AboveAllGyprockPlastering_14-10-2021.txt"
                Dim fname As String = Path.GetFileName(sFile).ToLower
                If Not (fname.Contains("_")) OrElse Not (fname.Contains(".txt")) Then
                    Continue For
                End If
                Dim Lines As String() = File.ReadAllLines(sFile)
                For Each Line As String In Lines
                    If Line.StartsWith("jpg opened") Then
                        'jpg opened October:  2020 ----0  <---Last Number
                        MonthToTake = Line.Split(":")(0).Replace("jpg opened", "").Trim '=October
                        YearToTake = Line.Split(":")(1).Trim.Substring(0, 4)
                        JpgOpened = CInt(Line.Substring(Line.IndexOf(YearToTake) + 5).Trim.Replace("-", ""))

                    ElseIf Line.StartsWith("pdf opened " & MonthToTake) AndAlso Line.Split(":")(1).Trim.StartsWith(YearToTake) Then
                        PdfOpened = CInt(Line.Substring(Line.IndexOf(YearToTake) + 5).Trim.Replace("-", ""))
                        Infos.Add(New UserInfo With {.UserName = CurrentUser, .MonthToTake = MonthToTake, .YearToTake = YearToTake, .JpgOpened = JpgOpened, .PdfOpened = PdfOpened})
                        CurMonthToTake = MonthToTake
                        CurYearToTake = YearToTake
                    End If
                Next 'Line

            Next 'File

        Next 'Folder
        If Infos.Count > 0 Then
            Dim res As New Dictionary(Of String, UserInfo)
            For Each Ui As UserInfo In Infos
                If res.ContainsKey(Ui.UserName & "|" & Ui.MonthToTake & "|" & Ui.YearToTake) Then
                    res(Ui.UserName & "|" & Ui.MonthToTake & "|" & Ui.YearToTake) = New UserInfo With {.JpgOpened = Ui.JpgOpened, .PdfOpened = Ui.PdfOpened}
                Else
                    res.Add(Ui.UserName & "|" & Ui.MonthToTake & "|" & Ui.YearToTake, New UserInfo With {.JpgOpened = Ui.JpgOpened, .PdfOpened = Ui.PdfOpened})
                End If
            Next
            For Each usr As String In lstUsers
                UpdateUI2(usr, FormatUser:=True)
                Dim MonthLine As String = ""
                Dim StartPos As List(Of Integer) = New List(Of Integer)
                Dim CurPos As Integer = 0
                Dim MonthTallied As List(Of String) = New List(Of String)
                StartPos.Add(CurPos)
                For Each key As String In res.Keys
                    Dim Ui2 As UserInfo = res(key)
                    If key.Split("|")(0) = usr Then
                        'MONTH 
                        Dim Mon As String = key.Split("|")(1).Substring(0, 3).ToUpper
                        MonthTallied.Add(Mon)
                        MonthLine &= Mon & " " & "JPG: " & Ui2.JpgOpened & ", PDF: " & Ui2.PdfOpened & vbTab
                        CurPos = MonthLine.Length
                        StartPos.Add(CurPos)
                    End If
                Next
                If MonthLine <> "" Then
                    For k = 1 To 12
                        Dim PrevMon As String = CDate("2020-" & k.ToString.PadLeft(2, "0") & "-01").ToString("MMM").ToUpper
                        If MonthLine.StartsWith(PrevMon) Then Exit For
                        If Not MonthTallied.Contains(PrevMon) Then
                            UpdateUI2(PrevMon & " " & "JPG: 0, PDF: 0" & vbTab, False, StartPos)
                            MonthTallied.Add(PrevMon)
                        End If
                    Next
                    UpdateUI2(MonthLine, False, StartPos)
                End If
            Next

        Else
            UpdateUI("No record found.")
        End If
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblTotal.Text = ""
        IniFileName = Application.StartupPath & "\FTPFileReader.ini"
        FTPHostName = IniFile.ReadFromINI("FTPHostName", IniFileName, "", "Setting")
        FTPRemoteFolder = IniFile.ReadFromINI("FTPRemoteFolder", IniFileName, "", "Setting")
        Me.txtFTPURL.Text = "ftp://" & FTPHostName & FTPRemoteFolder

        FTPUserName = IniFile.ReadFromINI("FTPUserName", IniFileName, "", "Setting")
        FTPPassword = IniFile.ReadFromINI("FTPPassword", IniFileName, "", "Setting")
        FTPFileFolder = IniFile.ReadFromINI("FTPFileFolder", IniFileName, "", "Setting")
        Me.txtFolder.Text = FTPFileFolder

        FolderBrowserDialog1.ShowNewFolderButton = False

        synchronizationContext = SynchronizationContext.Current
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Dim result As DialogResult = FolderBrowserDialog1.ShowDialog()
        If result = DialogResult.OK Then
            Me.txtFolder.Text = FolderBrowserDialog1.SelectedPath
            FTPFileFolder = Me.txtFolder.Text
            IniFile.WriteToINI("FTPFileFolder", FTPFileFolder, IniFileName, "Setting")
        End If

    End Sub

    Private Sub txtFolder_Leave(sender As Object, e As EventArgs) Handles txtFolder.Leave
        FTPFileFolder = Me.txtFolder.Text.Trim
        IniFile.WriteToINI("FTPFileFolder", FTPFileFolder, IniFileName, "Setting")
    End Sub
    Public Function DownloadFTPFolder() As Integer
        Try
            ' Setup session options
            Dim sessionOptions As New SessionOptions
            With sessionOptions
                .Protocol = Protocol.Ftp
                .HostName = FTPHostName '"example.com"
                .UserName = FTPUserName '"user"
                .Password = FTPPassword '"mypassword"
            End With

            Using session As New Session
                ' Will continuously report progress of synchronization
                AddHandler session.FileTransferred, AddressOf FileTransferred

                ' Connect
                session.Open(sessionOptions)

                ' Synchronize files
                Dim synchronizationResult As SynchronizationResult
                'synchronizationResult = session.SynchronizeDirectories(SynchronizationMode.Local, "d:\www", "/home/martin/public_html", False)
                synchronizationResult = session.SynchronizeDirectories(SynchronizationMode.Local, FTPFileFolder, FTPRemoteFolder, False)

                ' Throw on any error
                synchronizationResult.Check()
            End Using

            Return 0
        Catch e As Exception
            Debug.WriteLine("Error: {0}", e)
            'Me.RichTextBox1.AppendText("Error: " & e.Message & vbNewLine)
            UpdateUI("Error: " & e.Message)
            Return 1
        End Try

    End Function

    Private Sub FileTransferred(ByVal sender As Object, ByVal e As TransferEventArgs)

        If e.Error Is Nothing Then
            'Debug.WriteLine("Download of {0} succeeded", e.FileName)
            'Me.RichTextBox1.AppendText("Download of " & e.FileName & " succeeded" & vbNewLine)
            UpdateUI("Download of " & e.FileName & " succeeded")
        Else
            'Debug.WriteLine("Download of {0} failed: {1}", e.FileName, e.Error)
            'Me.RichTextBox1.AppendText("Download of " & e.FileName & " failed: " & e.Error.Message & vbNewLine)
            UpdateUI("Download of " & e.FileName & " failed: " & e.Error.Message)
        End If

        If e.Chmod IsNot Nothing Then
            If e.Chmod.Error Is Nothing Then
                Debug.WriteLine("Permisions of {0} set to {1}", e.Chmod.FileName, e.Chmod.FilePermissions)
            Else
                Debug.WriteLine("Setting permissions of {0} failed: {1}", e.Chmod.FileName, e.Chmod.Error)
            End If
        Else
            Debug.WriteLine("Permissions of {0} kept with their defaults", e.Destination)
        End If

        If e.Touch IsNot Nothing Then
            If e.Touch.Error Is Nothing Then
                Debug.WriteLine("Timestamp of {0} set to {1}", e.Touch.FileName, e.Touch.LastWriteTime)
            Else
                Debug.WriteLine("Setting timestamp of {0} failed: {1}", e.Touch.FileName, e.Touch.Error)
            End If
        Else
            ' This should never happen during "local to remote" synchronization
            Debug.WriteLine("Timestamp of {0} kept with its default (current time)", e.Destination)
        End If

    End Sub

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        If FTPHostName = "" OrElse FTPRemoteFolder = "" Then
            MessageBox.Show("Please fill FTP Setting.")
            Return
        Else
            If Me.txtFolder.Text.Trim = "" Then
                MessageBox.Show("Please browse to the local FTP File Folder.")
                Return
            Else
                If Not Directory.Exists(Me.txtFolder.Text.Trim) Then
                    If MessageBox.Show("Folder does not exist. Do you want to create folder " & Me.txtFolder.Text.Trim & "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                        Return
                    Else
                        Try
                            Directory.CreateDirectory(Me.txtFolder.Text.Trim)
                        Catch ex As Exception
                            MessageBox.Show("Could not create folder " & Me.txtFolder.Text.Trim)
                            Return
                        End Try

                    End If
                End If
            End If

            If MessageBox.Show("Do you want to download files from remote FTP folder .../" & FTPRemoteFolder.Substring(FTPRemoteFolder.LastIndexOf("/") + 1) & " to " & Me.txtFolder.Text & "? Existing files in the folder will be updated.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Return
            End If

        End If
        If DownloadFTPFolder() = 0 Then
            UpdateUI("---FTP Download Done.")
            btnStart_Click(sender, e)
        Else
            UpdateUI("---FTP Download Failed.")
        End If
        '---


    End Sub

    Private Sub btnFTPSetting_Click(sender As Object, e As EventArgs) Handles btnFTPSetting.Click
        Dim f As New frmFTPSetting
        f.ShowDialog()
        'like Form_Load
        Me.txtFTPURL.Text = "ftp://" & FTPHostName & FTPRemoteFolder
    End Sub
    Public Sub UpdateUI(ByVal value As String)
        synchronizationContext.Post(New SendOrPostCallback(Sub(o)
                                                               Me.RichTextBox1.AppendText(o.ToString & vbNewLine)

                                                           End Sub), value)
    End Sub
    Public Sub UpdateUI2Replace(ByVal value As String, ByVal byvalue As String)
        synchronizationContext.Post(New SendOrPostCallback(Sub(o)
                                                               Me.RichTextBox2.Text = Me.RichTextBox2.Text.Replace(value, byvalue)

                                                           End Sub), value)
    End Sub
    Public Sub UpdateUI2Format(ByVal value As String)
        synchronizationContext.Post(New SendOrPostCallback(Sub(o)
                                                               Debug.WriteLine("MonthLineNos.Count=" & Me.MonthLineNos.Count)
                                                               Debug.WriteLine("RichTextBox2.Text.Split(vbNewLine).Count=" & Me.RichTextBox2.Text.Split(vbNewLine).Count)
                                                               For Each LineNo As Integer In Me.MonthLineNos
                                                                   Dim MySelectionStart As Integer = 0
                                                                   Debug.WriteLine("LineNo=" & LineNo)
                                                                   Dim LineNo0 As Integer = LineNo
                                                                   For w = 0 To 2 '0 '2
                                                                       '3 lines for 12 months
                                                                       MySelectionStart = 0
                                                                       LineNo = LineNo0 + w '+= w
                                                                       For j = 1 To LineNo - 1
                                                                           Debug.WriteLine("j=" & j)
                                                                           Try
                                                                               MySelectionStart += RichTextBox2.Lines(j - 1).Length + 1

                                                                           Catch ex As Exception

                                                                           End Try

                                                                       Next
                                                                       Try
                                                                           RichTextBox2.SelectionStart = MySelectionStart
                                                                           RichTextBox2.SelectionLength = 3
                                                                           RichTextBox2.SelectionColor = Color.Chocolate
                                                                           RichTextBox2.SelectionFont = New Font(RichTextBox2.Font, FontStyle.Bold)
                                                                           'next months in line
                                                                           Dim NextTextInLine As String = RichTextBox2.Lines(LineNo - 1)
                                                                           Dim NextPosInLine As Integer = 0
                                                                           For f = 1 To 3 'MONTHS IN LINE
                                                                               Debug.WriteLine("NextTextInLineOLD=" & NextTextInLine)
                                                                               If f = 3 Then
                                                                                   NextPosInLine += NextTextInLine.IndexOf(vbTab)
                                                                                   RichTextBox2.SelectionStart = MySelectionStart + 1 + RichTextBox2.Lines(LineNo - 1).Substring(0, RichTextBox2.Lines(LineNo - 1).Length - 1).LastIndexOf(vbTab)
                                                                                   RichTextBox2.SelectionLength = 3
                                                                                   RichTextBox2.SelectionColor = Color.Chocolate
                                                                                   RichTextBox2.SelectionFont = New Font(RichTextBox2.Font, FontStyle.Bold)
                                                                               Else
                                                                                   NextPosInLine += NextTextInLine.IndexOf(vbTab) + 1 ' + f - 2 'Not +=
                                                                                   Debug.WriteLine("NextPosInLine=" & NextPosInLine)
                                                                                   Debug.WriteLine("f=" & f)
                                                                                   NextTextInLine = NextTextInLine.Substring(NextPosInLine)
                                                                                   RichTextBox2.SelectionStart = MySelectionStart + NextPosInLine
                                                                                   Debug.WriteLine("NextTextInLineNEW=" & NextTextInLine)
                                                                                   Debug.WriteLine("MON=" & RichTextBox2.Text.Substring(RichTextBox2.SelectionStart, 3))
                                                                                   RichTextBox2.SelectionLength = 3
                                                                                   RichTextBox2.SelectionColor = Color.Chocolate
                                                                                   RichTextBox2.SelectionFont = New Font(RichTextBox2.Font, FontStyle.Bold)
                                                                               End If

                                                                           Next
                                                                       Catch ex As Exception

                                                                       End Try
                                                                   Next
                                                               Next
                                                           End Sub), value)
    End Sub
    Public Sub UpdateUI2(ByVal value As String, Optional ByVal FormatUser As Boolean = False, Optional ByVal StartPos As List(Of Integer) = Nothing)
        synchronizationContext.Post(New SendOrPostCallback(Sub(o)
                                                               Dim BeginningIndex As Integer = Me.RichTextBox2.Text.Length
                                                               If value.StartsWith("JAN ") Then Me.MonthLineNos.Add(Me.RichTextBox2.Lines.Count)

                                                               Me.RichTextBox2.AppendText(o.ToString & vbNewLine)
                                                               If FormatUser Then
                                                                   RichTextBox2.SelectionStart = BeginningIndex
                                                                   RichTextBox2.SelectionLength = o.ToString.Length

                                                                   RichTextBox2.SelectionColor = Color.Blue
                                                                   RichTextBox2.SelectionFont = New Font(RichTextBox2.Font, FontStyle.Bold)
                                                               End If
                                                               If Not StartPos Is Nothing Then
                                                                   'months
                                                                   For i = 0 To StartPos.Count - 2
                                                                       RichTextBox2.Select(BeginningIndex + StartPos(i), 3)
                                                                       RichTextBox2.SelectionColor = Color.Chocolate
                                                                   Next
                                                               End If
                                                             
                                                           End Sub), value)
    End Sub
    Public Sub UpdateUI3(ByVal value As String, Optional ByVal FormatUser As Boolean = False, Optional ByVal StartPos As List(Of Integer) = Nothing)
        synchronizationContext.Post(New SendOrPostCallback(Sub(o)
                                                               Me.RichTextBox3.AppendText(o.ToString & vbNewLine)
                                                               Dim MONs As List(Of String) = New List(Of String)
                                                               Dim TotalJ As Integer = 0
                                                               Dim TotalP As Integer = 0
                                                               For i = 1 To 12
                                                                   Dim MON As String = CDate("2020-" & i.ToString.PadLeft(2, "0") & "-01").ToString("MMM").ToUpper
                                                                   MONs.Add(MON)
                                                                   Dim JMON As String = "_J" & CDate("2020-" & i.ToString.PadLeft(2, "0") & "-01").ToString("MMM").ToUpper
                                                                   Dim PMON As String = "_P" & CDate("2020-" & i.ToString.PadLeft(2, "0") & "-01").ToString("MMM").ToUpper
                                                                   Dim info As UserInfo = If(TallyInfo.ContainsKey(MON), TallyInfo(MON), New UserInfo With {.JpgOpened = 0, .PdfOpened = 0})
                                                                   TotalJ += info.JpgOpened
                                                                   TotalP += info.PdfOpened
                                                                   Me.RichTextBox3.Text = Me.RichTextBox3.Text.Replace(JMON, info.JpgOpened)
                                                                   Me.RichTextBox3.Text = Me.RichTextBox3.Text.Replace(PMON, info.PdfOpened)
                                                               Next
                                                               Me.lblTotal.Text = "Total JPEG:" & TotalJ & ", PDF:" & TotalP

                                                               For Each MON As String In MONs
                                                                   RichTextBox3.SelectionStart = Me.RichTextBox3.Text.IndexOf(MON)
                                                                   RichTextBox3.SelectionLength = 3
                                                                   RichTextBox3.SelectionColor = Color.Blue
                                                                   RichTextBox3.SelectionFont = New Font(RichTextBox3.Font, FontStyle.Bold)
                                                               Next


                                                           End Sub), value)
    End Sub

    Private Sub btnBrowseFTP_Click(sender As Object, e As EventArgs) Handles btnBrowseFTP.Click
        Dim fd As OpenFileDialog = New OpenFileDialog()
        Dim strFileName As String
        fd.Title = "Specify File for FPT Setting"
        'fd.InitialDirectory = "C:\"
        fd.Filter = "Text files (*.txt)|*.txt"
        fd.RestoreDirectory = True
        If fd.ShowDialog() = DialogResult.OK Then
            strFileName = fd.FileName
            'ftp://166.62.27.182:21/public_html/SUBS

            FTPHostName = IniFile.ReadFromINI("FTPHostName", strFileName, "", "Setting")
            FTPRemoteFolder = IniFile.ReadFromINI("FTPRemoteFolder", strFileName, "", "Setting")
            FTPUserName = IniFile.ReadFromINI("FTPUserName", strFileName, "", "Setting")
            FTPPassword = IniFile.ReadFromINI("FTPPassword", strFileName, "", "Setting")
            IniFile.WriteToINI("FTPHostName", FTPHostName, IniFileName, "Setting")
            IniFile.WriteToINI("FTPRemoteFolder", FTPRemoteFolder, IniFileName, "Setting")
            IniFile.WriteToINI("FTPUserName", FTPUserName, IniFileName, "Setting")
            IniFile.WriteToINI("FTPPassword", FTPPassword, IniFileName, "Setting")
            Me.txtFTPURL.Text = "ftp://" & FTPHostName & FTPRemoteFolder
            'MessageBox.Show("Saved!")
        End If
    End Sub
End Class
Public Class UserInfo
    Private userNameValue As String
    Public Property UserName() As String
        Get
            Return userNameValue
        End Get
        Set(ByVal value As String)
            userNameValue = value
        End Set
    End Property
    Private monthToTakeValue As String
    Public Property MonthToTake() As String
        Get
            Return monthToTakeValue
        End Get
        Set(ByVal value As String)
            monthToTakeValue = value
        End Set
    End Property
    Private yearToTakeValue As String
    Public Property YearToTake() As String
        Get
            Return yearToTakeValue
        End Get
        Set(ByVal value As String)
            yearToTakeValue = value
        End Set
    End Property
    Private jpgOpenedValue As Integer
    Public Property JpgOpened() As Integer
        Get
            Return jpgOpenedValue
        End Get
        Set(ByVal value As Integer)
            jpgOpenedValue = value
        End Set
    End Property
    Private pdfOpenedValue As Integer
    Public Property PdfOpened() As Integer
        Get
            Return pdfOpenedValue
        End Get
        Set(ByVal value As Integer)
            pdfOpenedValue = value
        End Set
    End Property
End Class
