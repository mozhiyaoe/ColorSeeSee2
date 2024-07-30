wscript.echo(WScript.Arguments.Item(0))
ImportPSD WScript.Arguments.Item(0),WScript.Arguments.Item(1)

Sub ImportPSD(sfilename, jsxName)

    Set WshShell = WScript.CreateObject("WScript.Shell")
    Set colProcessList = GetObject("Winmgmts:").ExecQuery("Select * from Win32_Process")

    Dim found
    found = False

    For Each objProcess In colProcessList
        If StrComp(objProcess.Name, "photoshop.exe", vbTextCompare) = 0 Then
            found = True
            Exit For
        End If
    Next

    Dim appRef
    If found Then
        Set appRef = GetObject(, "Photoshop.Application")
    Else
        Set appRef = CreateObject("Photoshop.Application")
    End If

    'Do While appRef.documents.Count
       'appRef.activeDocument.Close 2 'dont' save
    'Loop

    Dim originalRulerUnits
    originalRulerUnits = appRef.Preferences.RulerUnits
    appRef.Preferences.RulerUnits = 1 'pixels

    Dim docRef
    Set docRef = appRef.Open(sfilename)

    appRef.DoJavaScriptFile jsxName

    docRef.Close 2 'dont' save

    appRef.Preferences.RulerUnits = originalRulerUnits

End Sub