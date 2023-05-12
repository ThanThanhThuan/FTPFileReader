Imports FTPFileReader.IniFile
Public Class frmFTPSetting
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        FTPHostName = Me.txtHostName.Text.Trim
        IniFile.WriteToINI("FTPHostName", FTPHostName, IniFileName, "Setting")
        FTPRemoteFolder = Me.txtRemoteFolder.Text.Trim
        IniFile.WriteToINI("FTPRemoteFolder", FTPRemoteFolder, IniFileName, "Setting")
        FTPUserName = Me.txtUserName.Text.Trim
        IniFile.WriteToINI("FTPUserName", FTPUserName, IniFileName, "Setting")
        FTPPassword = Me.txtPW.Text.Trim
        IniFile.WriteToINI("FTPPassword", FTPPassword, IniFileName, "Setting")
        MessageBox.Show("Saved!")

    End Sub

    Private Sub frmFTPSetting_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.txtHostName.Text = FTPHostName
        Me.txtRemoteFolder.Text = FTPRemoteFolder
        Me.txtUserName.Text = FTPUserName
        Me.txtPW.Text = FTPPassword
    End Sub
End Class