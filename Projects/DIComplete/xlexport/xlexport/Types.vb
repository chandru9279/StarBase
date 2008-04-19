Public Class Types
    Public Structure Input
        'struct Input
        '{
        '    internal double principal;
        '    internal double startRate;
        '    internal DateTime startDate;
        '    internal DateTime endDate;
        '    internal List<InputTransaction> transactions;
        '}
        Public principal As Double
        Public startRate As Double
        Public startDate As DateTime
        Public endDate As DateTime
        Public transactions As List(Of InputTransaction)
    End Structure

    Public Structure InputTransaction
        'struct InputTransaction : IComparable<InputTransaction>
        '{
        '    internal TransactionType type;
        '    internal DateTime date;
        '    internal double amount;
        '}
        Public TransactionType As TransactionType
        Public _date As DateTime
        Public amount As Double
    End Structure

    Enum TransactionType
        'Enum TransactionType
        '{
        '    Invalid,
        '    Credit,
        '    Debit,
        '    Int,
        '    Cmt,
        '    Rate
        '}
        Invalid
        Credit
        Debit
        Int
        Cmt
        Rate
    End Enum


    Public Structure OutputTransaction
        'struct OutputTransaction
        '{
        '    internal TransactionType type;
        '    internal DateTime date;
        '    internal double initialPrincipal;
        '    internal double balancePrincipal;        
        '}
        Public type As TransactionType
        Public _date As DateTime
        Public initialPrincipal As Double
        Public balancePrincipal As Double
    End Structure

    Public Structure InterimInterest
        'Class InterimInterest
        '{
        '    internal DateTime startDate;
        '    internal DateTime endDate;
        '    internal double principal;
        '    internal double rate;
        '    internal double amount;        
        '}
        Public startDate As DateTime
        Public endDate As DateTime
        Public principal As Double
        Public rate As Double
        Public amount As Double
    End Structure

    Public Structure Output
        'struct Output
        '{
        '    internal double balancePrincipal;
        '    internal List<OutputTransaction> transactions;
        '    internal List<InterimInterest> interimInterests;
        '}
        Public balancePrincipal As Double
        Public transactions As List(Of OutputTransaction)
        Public interimInterests As List(Of InterimInterest)
    End Structure
    
End Class
