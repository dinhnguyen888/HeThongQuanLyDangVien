Dim fso, shell, scriptDir, exePath

Set fso = CreateObject("Scripting.FileSystemObject")
Set shell = CreateObject("WScript.Shell")

scriptDir = fso.GetParentFolderName(WScript.ScriptFullName)
exePath = fso.BuildPath(fso.BuildPath(scriptDir, "Build"), "QuanLyDangVien.exe")

If fso.FileExists(exePath) Then
    shell.Run """" & exePath & """", 0, False
Else
    MsgBox "Loi: Khong tim thay QuanLyDangVien.exe trong thu muc Build." & vbCrLf & _
           "Duong dan: " & exePath, vbOKOnly, "QuanLyDangVien"
End If


