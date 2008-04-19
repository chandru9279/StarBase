Imports System.Linq
Imports System.Collections.Generic
Imports <xmlns:o="urn:schemas-microsoft-com:office:office">
Imports <xmlns:x="urn:schemas-microsoft-com:office:excel">
Imports <xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet">
Imports <xmlns:html="http://www.w3.org/TR/REC-html40">


Public Class xlexport

    Public Function export(ByVal Input As Types.Input, _
                           ByVal Output As Types.Output, _
                           ByVal file As String _
                           ) As Integer

        Dim retcode = 0
        Try
            Dim XL = <?xml version="1.0"?>
                     <?mso-application progid="Excel.Sheet"?>
                     <Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet"
                         xmlns:o="urn:schemas-microsoft-com:office:office"
                         xmlns:x="urn:schemas-microsoft-com:office:excel"
                         xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
                         xmlns:html="http://www.w3.org/TR/REC-html40">
                         <DocumentProperties xmlns="urn:schemas-microsoft-com:office:office">
                             <Author>Home</Author>
                             <LastAuthor>Home</LastAuthor>
                             <Created>2007-12-24T06:51:28Z</Created>
                             <LastSaved>2007-12-24T08:16:18Z</LastSaved>
                             <Version>12.00</Version>
                         </DocumentProperties>
                         <ExcelWorkbook xmlns="urn:schemas-microsoft-com:office:excel">
                             <WindowHeight>11985</WindowHeight>
                             <WindowWidth>19095</WindowWidth>
                             <WindowTopX>120</WindowTopX>
                             <WindowTopY>45</WindowTopY>
                             <ProtectStructure>False</ProtectStructure>
                             <ProtectWindows>False</ProtectWindows>
                         </ExcelWorkbook>
                         <Styles>
                             <Style ss:ID="Default" ss:Name="Normal">
                                 <Alignment ss:Vertical="Bottom"/>
                                 <Borders/>
                                 <Font ss:FontName="Calibri" x:Family="Swiss" ss:Size="11" ss:Color="#000000"/>
                                 <Interior/>
                                 <NumberFormat/>
                                 <Protection/>
                             </Style>
                         </Styles>
                         <Worksheet ss:Name="Input">
                             <Table ss:ExpandedColumnCount="4" ss:ExpandedRowCount=<%= Input.transactions.Count + 7 %> x:FullColumns="1"
                                 x:FullRows="1" ss:DefaultRowHeight="15">
                                 <Column ss:AutoFitWidth="1"/>
                                 <Column ss:AutoFitWidth="1"/>
                                 <Column ss:AutoFitWidth="1"/>
                                 <Column ss:AutoFitWidth="1"/>
                                 <Row ss:AutoFitHeight="0">
                                     <Cell><Data ss:Type="String">Principal</Data></Cell>
                                     <Cell><Data ss:Type="Number"><%= Input.principal %></Data></Cell>
                                 </Row>
                                 <Row ss:AutoFitHeight="0">
                                     <Cell><Data ss:Type="String">StartRate</Data></Cell>
                                     <Cell><Data ss:Type="Number"><%= Input.startRate %></Data></Cell>
                                 </Row>
                                 <Row ss:AutoFitHeight="0">
                                     <Cell><Data ss:Type="String">StartDate</Data></Cell>
                                     <Cell><Data ss:Type="String"><%= Input.startDate.ToString("d") %></Data></Cell>
                                 </Row>
                                 <Row ss:AutoFitHeight="0">
                                     <Cell><Data ss:Type="String">EndDate</Data></Cell>
                                     <Cell><Data ss:Type="String"><%= Input.endDate.Date.ToString("d") %></Data></Cell>
                                 </Row>
                                 <Row ss:Index="6" ss:AutoFitHeight="0">
                                     <Cell><Data ss:Type="String">Transactions  :</Data></Cell>
                                 </Row>
                                 <Row ss:AutoFitHeight="0">
                                     <Cell><Data ss:Type="String">Type</Data></Cell>
                                     <Cell><Data ss:Type="String">Field 1</Data></Cell>
                                     <Cell><Data ss:Type="String">Field 2</Data></Cell>
                                 </Row>
                                 <%= _
                                     From it In Input.transactions _
                                     Select <Row ss:AutoFitHeight="0" xmlns="urn:schemas-microsoft-com:office:spreadsheet">
                                                <Cell><Data ss:Type="String"><%= it.TransactionType.ToString() %></Data></Cell>
                                                <Cell><Data ss:Type="String"><%= it._date.Date.ToString("d") %></Data></Cell>
                                                <Cell><Data ss:Type="Number"><%= it.amount %></Data></Cell>
                                            </Row> _
                                 %>
                             </Table>
                             <WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">
                                 <PageSetup>
                                     <Header x:Margin="0.3"/>
                                     <Footer x:Margin="0.3"/>
                                     <PageMargins x:Bottom="0.75" x:Left="0.7" x:Right="0.7" x:Top="0.75"/>
                                 </PageSetup>
                                 <Unsynced/>
                                 <TabColorIndex>63</TabColorIndex>
                                 <Selected/>
                                 <Panes>
                                     <Pane>
                                         <Number>3</Number>
                                         <ActiveRow>6</ActiveRow>
                                         <ActiveCol>2</ActiveCol>
                                     </Pane>
                                 </Panes>
                                 <ProtectObjects>False</ProtectObjects>
                                 <ProtectScenarios>False</ProtectScenarios>
                             </WorksheetOptions>
                         </Worksheet>
                         <Worksheet ss:Name="Interests">
                             <Table ss:ExpandedColumnCount="5" ss:ExpandedRowCount=<%= Output.interimInterests.Count + 2 %> x:FullColumns="1"
                                 x:FullRows="1" ss:DefaultRowHeight="15">
                                 <Row ss:AutoFitHeight="0">
                                     <Cell><Data ss:Type="String">StartDate</Data></Cell>
                                     <Cell><Data ss:Type="String">EndDate</Data></Cell>
                                     <Cell><Data ss:Type="String">Principal</Data></Cell>
                                     <Cell><Data ss:Type="String">Rate</Data></Cell>
                                     <Cell><Data ss:Type="String">Amount</Data></Cell>
                                 </Row>
                                 <Row/>
                                 <%= _
                                     From ii In Output.interimInterests _
                                     Select <Row ss:AutoFitHeight="0" xmlns="urn:schemas-microsoft-com:office:spreadsheet">
                                                <Cell><Data ss:Type="String"><%= ii.startDate.Date.ToString("d") %></Data></Cell>
                                                <Cell><Data ss:Type="String"><%= ii.endDate.Date.ToString("d") %></Data></Cell>
                                                <Cell><Data ss:Type="Number"><%= ii.principal %></Data></Cell>
                                                <Cell><Data ss:Type="Number"><%= ii.rate %></Data></Cell>
                                                <Cell><Data ss:Type="Number"><%= ii.amount %></Data></Cell>
                                            </Row> _
                                 %>
                             </Table>
                             <WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">
                                 <PageSetup>
                                     <Header x:Margin="0.3"/>
                                     <Footer x:Margin="0.3"/>
                                     <PageMargins x:Bottom="0.75" x:Left="0.7" x:Right="0.7" x:Top="0.75"/>
                                 </PageSetup>
                                 <Unsynced/>
                                 <Print>
                                     <ValidPrinterInfo/>
                                     <VerticalResolution>0</VerticalResolution>
                                 </Print>
                                 <TabColorIndex>21</TabColorIndex>
                                 <Panes>
                                     <Pane>
                                         <Number>3</Number>
                                         <ActiveCol>4</ActiveCol>
                                     </Pane>
                                 </Panes>
                                 <ProtectObjects>False</ProtectObjects>
                                 <ProtectScenarios>False</ProtectScenarios>
                             </WorksheetOptions>
                         </Worksheet>
                         <Worksheet ss:Name="Output">
                             <Table ss:ExpandedColumnCount="4" ss:ExpandedRowCount=<%= Output.transactions.Count + 4 %> x:FullColumns="1"
                                 x:FullRows="1" ss:DefaultRowHeight="15">
                                 <Column ss:Index="3" ss:AutoFitWidth="0" ss:Width="75.75"/>
                                 <Column ss:AutoFitWidth="0" ss:Width="101.25"/>
                                 <Row ss:AutoFitHeight="0">
                                     <Cell><Data ss:Type="String">Date</Data></Cell>
                                     <Cell><Data ss:Type="String">Type</Data></Cell>
                                     <Cell><Data ss:Type="String">InitialPrincipal</Data></Cell>
                                     <Cell><Data ss:Type="String">BalancePrincipal</Data></Cell>
                                 </Row>
                                 <Row/>
                                 <%= _
                                     From ot In Output.transactions _
                                     Select <Row ss:AutoFitHeight="0" xmlns="urn:schemas-microsoft-com:office:spreadsheet">
                                                <Cell><Data ss:Type="String"><%= ot._date.Date.ToString("d") %></Data></Cell>
                                                <Cell><Data ss:Type="String"><%= ot.type.ToString() %></Data></Cell>
                                                <Cell><Data ss:Type="Number"><%= ot.initialPrincipal %></Data></Cell>
                                                <Cell><Data ss:Type="Number"><%= ot.balancePrincipal %></Data></Cell>
                                            </Row> _
                                 %>
                                 <Row/>
                                 <Row ss:AutoFitHeight="0">
                                     <Cell/>
                                     <Cell/>
                                     <Cell><Data ss:Type="String">Balance Principal:</Data></Cell>
                                     <Cell><Data ss:Type="Number"><%= Output.balancePrincipal %></Data></Cell>
                                 </Row>
                             </Table>
                             <WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">
                                 <PageSetup>
                                     <Header x:Margin="0.3"/>
                                     <Footer x:Margin="0.3"/>
                                     <PageMargins x:Bottom="0.75" x:Left="0.7" x:Right="0.7" x:Top="0.75"/>
                                 </PageSetup>
                                 <Unsynced/>
                                 <Print>
                                     <ValidPrinterInfo/>
                                     <VerticalResolution>0</VerticalResolution>
                                 </Print>
                                 <TabColorIndex>10</TabColorIndex>
                                 <Panes>
                                     <Pane>
                                         <Number>3</Number>
                                         <RangeSelection>R1C1:R1C4</RangeSelection>
                                     </Pane>
                                 </Panes>
                                 <ProtectObjects>False</ProtectObjects>
                                 <ProtectScenarios>False</ProtectScenarios>
                             </WorksheetOptions>
                         </Worksheet>
                     </Workbook>
            Try
                XL.Save(file)
            Catch ex As ApplicationException
                retcode = -2
            End Try
        Catch ex As ApplicationException
            retcode = -1
        End Try

        Return retcode
    End Function
End Class
