Public Class SerialPortForm
    Private Sub SerialPortForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect()
        Timer.Interval = 500 'Set Timer interval to 500 ms
        Timer.Start()
        'for each timer tick, call ShiftLED
    End Sub

    Sub Connect()
        SerialPort1.Close()
        SerialPort1.BaudRate = 115200 'Q@ Board Default
        SerialPort1.Parity = IO.Ports.Parity.None
        SerialPort1.StopBits = IO.Ports.StopBits.One
        SerialPort1.DataBits = 8
        SerialPort1.PortName = "COM3" 'Change to your COM Port

        SerialPort1.Open()
        If SerialPort1.IsOpen Then
            MessageBox.Show("Connected to " & SerialPort1.PortName)
        End If
        SerialPort1.Close()
    End Sub

    Private Sub Close_Button_Click(sender As Object, e As EventArgs) Handles Close_Button.Click
        SerialPort1.Close()
        Me.Close()
    End Sub

    Sub ShiftLED()
        SerialPort1.Close()
        SerialPort1.BaudRate = 115200 'Q@ Board Default
        SerialPort1.Parity = IO.Ports.Parity.None
        SerialPort1.StopBits = IO.Ports.StopBits.One
        SerialPort1.DataBits = 8
        SerialPort1.PortName = "COM3" 'Change to your COM Port
        SerialPort1.Open()
        Dim data As Byte() = New Byte(1) {}
        data(0) = &H20 'First byte
        Dim count = CountIncrease() 'Increase count
        Select Case count
            Case 1
                data(1) = &H1
            Case 2
                data(1) = &H2
            Case 3
                data(1) = &H4
            Case 4
                data(1) = &H8
            Case 5
                data(1) = &H10
            Case 6
                data(1) = &H20
            Case 7
                data(1) = &H40
            Case 8
                data(1) = &H80
        End Select


        SerialPort1.Write(data, 0, 2) 'Send 2 bytes of data
        SerialPort1.Close()
    End Sub

    Function CountIncrease() As Integer
        Static count As Integer = 1
        count += 1
        If count > 8 Then
            count = 1
        End If
        Return count
    End Function

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        ShiftLED()
    End Sub
End Class
