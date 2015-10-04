Imports System.IO
Imports System.Collections

Module Module1

    Dim CurrentFileWriter As StreamWriter
    Dim CurrentFileReader As StreamReader
    Dim Directory As String = "C:\Users\CIC1\Desktop\"
    Dim FileName As String = ""
    Dim Line As String = ""
    Dim Document As New ArrayList()
    Dim Opt As Integer

    Sub Main()      'Unfinished more Testing / Debugging Needed

        Console.Clear()
        Console.Title = "Enio's Simple Text Editor"
        Console.WriteLine() : Console.WriteLine()
        Console.WriteLine("     New Doc          :   2")
        Console.WriteLine("     Open Doc         :   4")
        Console.WriteLine("     Set File Path    :   6")
        Console.WriteLine("     Set File Name    :   8")
        Console.WriteLine("     Exit             :   0")
        Console.WriteLine() : Console.WriteLine()
        Console.Write("Select Option: ")
        Try
            Opt = Console.ReadLine()
        Catch
            Main()
        End Try
        Console.Clear()

        Select Case Opt
            Case 2
                NewFile() : Console.Clear()
            Case 4
                OpenFile() : Console.Clear()
            Case 6
                SetFilePath() : Console.Clear()
            Case 8
                SetFileName() : Console.Clear()
            Case 0
                Exit Sub
        End Select
    End Sub

    Sub SetFilePath()
        Dim temp As String
        Console.WriteLine("Press 0 to Keep Path and Return to Main Menu")
        Console.WriteLine("Current file Path: " & Directory)
        Console.Write("New file Path: ")
        temp = Console.ReadLine
        If temp = "0" Then
            Main()
        Else
            Directory = temp
        End If
        Main()
    End Sub

    Sub SetFileName()
        Dim temp As String
        Do
            Console.WriteLine("Press 0 to Keep File Name and Return to Main Menu")
            If FileName = "" Then
                Console.WriteLine("File Name NOT Current Set!")
            Else
                Console.WriteLine("Current file name: " & FileName)
            End If
            Console.Write("New file name: ")
            temp = Console.ReadLine
            If temp = "0" Then
                Main()
            Else
                FileName = temp
            End If
            Console.Clear()
        Loop While temp <> "0"
    End Sub

    Sub NewFile()
        Dim i As Integer = 1
        Dim Finish As Boolean = False
        Dim temp As String
        Document.Clear()

        Console.WriteLine("Creating new text file")
        Console.WriteLine()
        If FileName = "" Then SetFileName()
        If Opt = 0 Then Exit Sub

        Try
            CurrentFileReader = New StreamReader(Directory & FileName & ".txt")
            CurrentFileReader.Close()
            Console.WriteLine("File Already Exists. If you Continue Current File will be Deleted!")
            Console.WriteLine("Do you wish to continue? Y / N ")
            temp = Console.ReadLine()
            If temp = "N" Or temp = "n" Then
                Console.Clear()
                FileName = ""
                SetFileName()
                If Opt = 0 Then Exit Sub
            End If
        Catch e As Exception
            Console.WriteLine("File OK")
        End Try

        CurrentFileWriter = New StreamWriter(Directory & FileName & ".txt")

        Console.WriteLine()
        Console.Title = "To Stop enter:    0-=    Found to the left of Backspace"
        Console.Clear()

        Do Until (Finish = True)
            Line = Console.ReadLine()
            If Line = "0-=" Then
                Finish = True
            Else
                Document.Add(Line)
                i += 1
            End If
        Loop

        For i = 0 To Document.Count - 1
            CurrentFileWriter.WriteLine(Document.Item(i))
        Next
        CurrentFileWriter.Close()
        Main()
    End Sub

    Sub ReadDoc()
        Dim i As Integer = 1
        Document.Clear()

        CurrentFileReader = New StreamReader(Directory & FileName & ".txt")
        Do Until CurrentFileReader.EndOfStream
            Line = CurrentFileReader.ReadLine
            Document.Add(Line)
            Console.Write(i & "   ")
            Console.WriteLine(Line)
            i += 1
        Loop
        CurrentFileReader.Close()
        EditFile()
    End Sub

    Sub EditFile()
        Dim L As Integer

        CurrentFileWriter = New StreamWriter(Directory & FileName & ".txt")
        Console.Title = "Enter 0 to go to Main Menu"
        If Document.Count = 0 Then
            Console.WriteLine("Document is Blank!, Press Enter to Continue")
            Console.ReadLine()
            Console.Clear()
        End If
        Console.Write("Enter Line to Edit or Enter " & Document.Count + 1 & " to Add New Line: ")
        L = Console.ReadLine()
        If L = 0 Then
        ElseIf L < Document.Count + 1 Then
            Document.Item(L - 1) = Console.ReadLine()
        ElseIf L > Document.Count Then
            Document.Add(Console.ReadLine())
        End If
        Dim i as integer
        For i = 0 To Document.Count - 1
            CurrentFileWriter.WriteLine(Document.Item(i))
        Next
        CurrentFileWriter.Close()
        Console.Clear()

        If L <> 0 Then
            ReadDoc()
        Else
            Main()
        End If
    End Sub

    Sub OpenFile()  ' Error for invalid File Name
        Console.WriteLine("Opening text file")
        Console.WriteLine()
        Try
            If FileName = "" Then SetFileName()
            CurrentFileReader = New StreamReader(Directory & FileName & ".txt")
            CurrentFileReader.Close()
            Console.Clear()
            ReadDoc()
        Catch e As Exception
            Console.Write("File Not Found: " & e.Message)
            Console.ReadLine()
            Console.Clear()
            FileName = ""
            OpenFile()
        End Try
    End Sub
End Module
