Dim fso, shell, scriptDir, exePath, wshShell

Set fso = CreateObject("Scripting.FileSystemObject")
Set shell = CreateObject("WScript.Shell")
Set wshShell = CreateObject("WScript.Shell")

scriptDir = fso.GetParentFolderName(WScript.ScriptFullName)
exePath = fso.BuildPath(fso.BuildPath(scriptDir, "Build"), "QuanLyDangVien.exe")

If Not fso.FileExists(exePath) Then
    MsgBox "Loi: Khong tim thay QuanLyDangVien.exe trong thu muc Build." & vbCrLf & _
           "Duong dan: " & exePath, vbOKOnly, "QuanLyDangVien"
    WScript.Quit
End If

' Khoi dong LocalDB instance truoc khi chay ung dung
On Error Resume Next
Dim result, checkResult, maxRetries, retryCount
maxRetries = 5
retryCount = 0

' Start LocalDB instance
result = wshShell.Run("sqllocaldb start MSSQLLocalDB", 0, True)
If Err.Number <> 0 Then
    Err.Clear
End If

' Kiem tra LocalDB da san sang chua (toi da 5 lan, moi lan doi 1 giay)
Do While retryCount < maxRetries
    checkResult = wshShell.Run("sqllocaldb info MSSQLLocalDB", 0, True)
    If checkResult = 0 Then
        ' LocalDB da san sang, doi them 1 giay de dam bao
        WScript.Sleep 1000
        Exit Do
    End If
    WScript.Sleep 1000
    retryCount = retryCount + 1
Loop

On Error Goto 0

' Chay ung dung
shell.Run """" & exePath & """", 0, False


