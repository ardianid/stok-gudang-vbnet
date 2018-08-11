Class Person
    Public Sub New(ByVal firstName As String, ByVal secondName As String)
        Me.FirstName = firstName
        Me.SecondName = secondName
        Me.Comments = String.Empty
    End Sub
    Public Sub New(ByVal firstName As String, ByVal secondName As String, ByVal comments As String)
        Me.New(firstName, secondName)
        Me.Comments = comments
    End Sub
    Public Property FirstName() As String
        Get
            Return Me._firstName
        End Get
        Set(ByVal value As String)
            Me._firstName = value
        End Set
    End Property
    Public Property SecondName() As String
        Get
            Return Me._secondName
        End Get
        Set(ByVal value As String)
            Me._secondName = value
        End Set
    End Property
    Public Property Comments() As String
        Get
            Return Me._comments
        End Get
        Set(ByVal value As String)
            Me._comments = value
        End Set
    End Property
    Private _comments As String
    Private _firstName As String
    Private _secondName As String
End Class
