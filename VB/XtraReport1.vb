Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.UI

Namespace RepAlignCheckBoxTableCell
	Partial Public Class XtraReport1
		Inherits DevExpress.XtraReports.UI.XtraReport
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub xrCheckBox1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles xrCheckBox1.BeforePrint
			Dim neighborCellText As String = GetCurrentColumnValue("Content").ToString()
			Dim graphics As Graphics = Graphics.FromHwnd(IntPtr.Zero)

			Dim neighborCellWidthPixels As Integer = ConvertFromReportUnitToPixels(xrTableCell2.WidthF)

			Dim neighborCellSizePixels As SizeF = graphics.MeasureString(neighborCellText, xrTableCell2.Font, neighborCellWidthPixels, BrickStringFormat.Create(xrTableCell2.TextAlignment, xrTableCell2.WordWrap).Value)

			graphics.Dispose()

			Dim neighborCellHeightReportUnits As Single = ConvertFromPixelsToReportUnit(CInt(Fix(Math.Round(neighborCellSizePixels.Height))))

            xrCheckBox1.LocationF = New PointF(xrCheckBox1.LocationF.X, neighborCellHeightReportUnits / 2 - xrCheckBox1.SizeF.Height / 2)
		End Sub

		Private Function ConvertFromReportUnitToPixels(ByVal value As Single) As Integer
			Dim unit As GraphicsUnit = (If(Me.ReportUnit = ReportUnit.HundredthsOfAnInch, GraphicsUnit.Inch, GraphicsUnit.Millimeter))
			Dim multiplier As Single = (If(unit = GraphicsUnit.Inch, 100, 10))

			Return CInt(Fix(Math.Round(GraphicsUnitConverter.Convert(value, unit, GraphicsUnit.Pixel) / multiplier)))
		End Function

		Private Function ConvertFromPixelsToReportUnit(ByVal value As Integer) As Single
			Dim unit As GraphicsUnit = (If(Me.ReportUnit = ReportUnit.HundredthsOfAnInch, GraphicsUnit.Inch, GraphicsUnit.Millimeter))
			Dim multiplier As Single = (If(unit = GraphicsUnit.Inch, 100, 10))

            Return GraphicsUnitConverter.Convert(Convert.ToSingle(value), GraphicsUnit.Pixel, unit) * multiplier
		End Function

	End Class

	Public Class ManualDataSet
		Inherits DataSet
		Public Sub New()
			MyBase.New()
			Dim table As New DataTable("table")

			DataSetName = "ManualDataSet"

			table.Columns.Add("ID", GetType(Int32))
			table.Columns.Add("Content", GetType(String))
			table.Columns.Add("Checked", GetType(Boolean))
			table.Constraints.Add("IDPK", table.Columns("ID"), True)

			Tables.AddRange(New DataTable() { table })
		End Sub

		Public Shared Function CreateData() As ManualDataSet
			Dim ds As New ManualDataSet()
			Dim table As DataTable = ds.Tables("table")

			table.Rows.Add(New Object() { 0, "Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed...", True })
			table.Rows.Add(New Object() { 1, "Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.", True })
			table.Rows.Add(New Object() { 2, "Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero eros et accumsan et iusto odio dignissim qui blandit praesent luptatum zzril delenit augue duis dolore te feugait nulla facilisi. Nam liber tempor cum soluta nobis eleifend option congue nihil imperdiet doming id quod mazim placerat facer possim assum. Typi non habent claritatem insitam; est usus legentis in iis qui facit eorum claritatem.", False })
			table.Rows.Add(New Object() { 4, "Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero eros et accumsan et iusto odio dignissim qui blandit praesent luptatum zzril delenit augue duis dolore te feugait nulla facilisi.", True })

			Return ds
		End Function

		#Region "Disable Serialization for Tables and Relations"
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Shadows ReadOnly Property Tables() As DataTableCollection
			Get
				Return MyBase.Tables
			End Get
		End Property

		<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Shadows ReadOnly Property Relations() As DataRelationCollection
			Get
				Return MyBase.Relations
			End Get
		End Property
		#End Region ' Disable Serialization for Tables and Relations
	End Class

End Namespace