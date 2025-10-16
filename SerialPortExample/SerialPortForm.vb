Public Class SerialPortForm
    Private Sub SerialPortForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect()   'Connect to Serial Port
        Timer.Interval = 500 'Set Timer interval to 500 ms
        Timer.Start()   'Start Timer
    End Sub

    Sub Connect()
        SerialPort1.Close()
        SerialPort1.BaudRate = 115200 'Q@ Board Default
        SerialPort1.Parity = IO.Ports.Parity.None   'No Parity
        SerialPort1.StopBits = IO.Ports.StopBits.One    '1 Stop Bit
        SerialPort1.DataBits = 8    '8 Data Bits
        SerialPort1.PortName = "COM3" 'Change to your COM Port

        SerialPort1.Open()  'Open Serial Port
        If SerialPort1.IsOpen Then  'Check if Serial Port is open
            MessageBox.Show("Connected to " & SerialPort1.PortName) 'Show message if connected
        End If
        SerialPort1.Close() 'Close Serial Port
    End Sub

    Private Sub Close_Button_Click(sender As Object, e As EventArgs) Handles Close_Button.Click
        SerialPort1.Close() 'Close Serial Port
        Me.Close()  'Close Form
    End Sub

    Sub ShiftLED()
        SerialPort1.Close()
        SerialPort1.BaudRate = 115200 'Q@ Board Default
        SerialPort1.Parity = IO.Ports.Parity.None   'No Parity
        SerialPort1.StopBits = IO.Ports.StopBits.One    '1 Stop Bit
        SerialPort1.DataBits = 8    '8 Data Bits
        SerialPort1.PortName = "COM3" 'Change to your COM Port
        SerialPort1.Open()  'Open Serial Port
        Dim data As Byte() = New Byte(1) {}
        data(0) = &H20 'First byte (code to control digital output LED)
        Dim count = CountIncrease() 'Increase count for case statement
        Select Case count
            Case 1
                data(1) = &H1   'Turn on LED1  (0000 0001)
            Case 2
                data(1) = &H2   'Turn on LED2  (0000 0010)
            Case 3
                data(1) = &H4   'Turn on LED3  (0000 0100)
            Case 4
                data(1) = &H8   'Turn on LED4  (0000 1000)
            Case 5
                data(1) = &H10  'Turn on LED5  (0001 0000)
            Case 6
                data(1) = &H20  'Turn on LED6  (0010 0000)
            Case 7
                data(1) = &H40  'Turn on LED7  (0100 0000)
            Case 8
                data(1) = &H80  'Turn on LED8  (1000 0000)
        End Select


        SerialPort1.Write(data, 0, 2) 'Send 2 bytes of data
        SerialPort1.Close() 'Close Serial Port
    End Sub

    Function CountIncrease() As Integer
        Static count As Integer = 1 'Static variable to hold count value
        count += 1 'Increase count
        If count > 8 Then
            count = 1 'Reset count after 8
        End If
        Return count    'Return count value
    End Function

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        ShiftLED()  'Timer runs on Tick Event every 500 ms
    End Sub
End Class
