'wscript.echo(WScript.Arguments.Item(0))
'ImportPSD WScript.Arguments.Item(0),WScript.Arguments.Item(1)
CopyCEP WScript.Arguments.Item(0),WScript.Arguments.Item(1)

Sub CopyCEP(ZipFile,apppath)

    Dim cepPath
    cepPath = apppath & "Required\CEP\extensions\Psd2ui"
	
	Dim cepRootPath
	cepRootPath = apppath & "Required\CEP\extensions"

    wscript.echo(cepPath)

    'WScript.Sleep(2000)

    dim filesys
    set filesys=CreateObject("Scripting.FileSystemObject")
    If filesys.FolderExists(cepPath) Then
        filesys.DeleteFolder(cepPath)
    End If

    
    wscript.echo("DeleteFolder")

    'The location of the zip file.
    'ZipFile="E:\ttgame\psd2ui_tool\UnityPsd2UI\Assets\Psd2UI\PhotoshopScripts\CEP.zip"
    'The folder the contents should be extracted to.

    'If the extraction location does not exist create it.
    Set fso = CreateObject("Scripting.FileSystemObject")
    If NOT fso.FolderExists(cepRootPath) Then
    fso.CreateFolder(cepRootPath)
    End If

    WScript.echo(ZipFile)
    'Extract the contants of the zip file.
    set objShell = CreateObject("Shell.Application")
    set FilesInZip=objShell.NameSpace(ZipFile).items
    objShell.NameSpace(cepRootPath).CopyHere(FilesInZip)
    Set fso = Nothing
    Set objShell = Nothing

    WScript.Sleep(10000)

    'WScript.Sleep(2000)
    'filesys.CopyFolder "c:\sourcefolder\website", "c:\destfolder\"

    'Do While appRef.documents.Count
       'appRef.activeDocument.Close 2 'dont' save
    'Loop

    'Dim originalRulerUnits
    'originalRulerUnits = appRef.Preferences.RulerUnits
    'appRef.Preferences.RulerUnits = 1 'pixels

    'Dim docRef
    'Set docRef = appRef.Open(sfilename)

    'appRef.DoJavaScriptFile jsxName

    'docRef.Close 2 'dont' save

    'appRef.Preferences.RulerUnits = originalRulerUnits

End Sub