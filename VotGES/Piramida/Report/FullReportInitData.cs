using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace VotGES.Piramida.Report
{
	public class FullReportRecord : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public void NotifyChanged(string propName) {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}
		public List<FullReportRecord> Children { get; set; }

		public string Title { get; set; }
		public string Key { get; set; }
		public bool IsGroup { get; set; }
		public bool Selectable { get; set; }
		protected bool selected;
		public bool Selected { get { return selected; } set { selected = value; NotifyChanged("Selected"); } }

		public FullReportRecord() { }

		public FullReportRecord addChild(FullReportRecord child) {
			Children.Add(child);
			IsGroup = true;
			return child;
		}
	}


	public class FullReportInitData
	{
		public FullReportRecord Root { get; set; }
		public FullReportInitData() {
			ReportGARecords.CreateGAP();
			ReportGARecords.CreateGAQ();
			ReportGlTransformRecords.CreateGlTransformP();
			ReportLinesRecords.CreateLinesP();
			ReportSNRecords.CreateSNP();
			ReportMainRecords.Create();
			ReportWaterRecords.CreateWater();


			FullReportRecord record,childRecord, child2, child3;
			Root = GetFullReportRecord("Воткинская ГЭС", "votGES");
			record=Root.addChild(GetFullReportRecord("Основные параметры", "mainParams"));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_GES));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_GTP1));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_GTP2));

			record = Root.addChild(GetFullReportRecord("Вода", "water"));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_Napor));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_NB));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_QGES));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_Temp));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_VB));

			record = Root.addChild(GetFullReportRecord("Генераторы", "generators"));
			childRecord = record.addChild(GetFullReportRecord("Генератор 1", "ga1"));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA1_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA1_Otd));
			childRecord.addChild(GetFullReportRecord(ReportGARecords.P_GA1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA1));

			childRecord = record.addChild(GetFullReportRecord("Генератор 2", "ga2"));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA2_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA2_Otd));
			childRecord.addChild(GetFullReportRecord(ReportGARecords.P_GA2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA2));

			childRecord = record.addChild(GetFullReportRecord("Генератор 3", "ga3"));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA3_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA3_Otd));
			childRecord.addChild(GetFullReportRecord(ReportGARecords.P_GA3));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA3));

			childRecord = record.addChild(GetFullReportRecord("Генератор 4", "ga4"));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA4_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA4_Otd));
			childRecord.addChild(GetFullReportRecord(ReportGARecords.P_GA4));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA4));

			childRecord = record.addChild(GetFullReportRecord("Генератор 5", "ga5"));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA5_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA5_Otd));
			childRecord.addChild(GetFullReportRecord(ReportGARecords.P_GA5));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA5));

			childRecord = record.addChild(GetFullReportRecord("Генератор 6", "ga6"));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA1_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA1_Otd));
			childRecord.addChild(GetFullReportRecord(ReportGARecords.P_GA1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA6));

			childRecord = record.addChild(GetFullReportRecord("Генератор 7", "ga7"));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA7_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA7_Otd));
			childRecord.addChild(GetFullReportRecord(ReportGARecords.P_GA7));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA7));

			childRecord = record.addChild(GetFullReportRecord("Генератор 8", "ga8"));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA8_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA8_Otd));
			childRecord.addChild(GetFullReportRecord(ReportGARecords.P_GA8));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA8));

			childRecord = record.addChild(GetFullReportRecord("Генератор 9", "ga9"));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA9_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA9_Otd));
			childRecord.addChild(GetFullReportRecord(ReportGARecords.P_GA9));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA9));

			childRecord = record.addChild(GetFullReportRecord("Генератор 10", "ga10"));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA10_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA10_Otd));
			childRecord.addChild(GetFullReportRecord(ReportGARecords.P_GA10));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA10));

			record = Root.addChild(GetFullReportRecord(ReportMainRecords.P_SP));
			record.addChild(GetFullReportRecord(ReportGARecords.P_Vozb));
			record.addChild(GetFullReportRecord(ReportGARecords.P_SN_GA));
			record.addChild(GetFullReportRecord(ReportGARecords.P_SK));
			childRecord = record.addChild(GetFullReportRecord(ReportMainRecords.P_Nebalans));

			child2=childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_T_Nebalans));
			child2.addChild(GetFullReportRecord(ReportGlTransformRecords.P_1T_Nebalans));
			child2.addChild(GetFullReportRecord(ReportGlTransformRecords.P_2AT_Nebalans));
			child2.addChild(GetFullReportRecord(ReportGlTransformRecords.P_3AT_Nebalans));
			child2.addChild(GetFullReportRecord(ReportGlTransformRecords.P_4T_Nebalans));
			child2.addChild(GetFullReportRecord(ReportGlTransformRecords.P_56AT_Nebalans));

			child2 = childRecord.addChild(GetFullReportRecord(ReportLinesRecords.P_VL_Nebalans));
			child2.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Nebalans));
			child2.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Nebalans));
			child2.addChild(GetFullReportRecord(ReportLinesRecords.P_VL500_Nebalans));

		}

		public static FullReportRecord GetFullReportRecord(string title, string id) {
			FullReportRecord record=new FullReportRecord();
			record.Title = title;
			record.Key = id;
			record.Selectable = false;
			record.Children = new List<FullReportRecord>();
			return record;
		}

		public static FullReportRecord GetFullReportRecord(RecordTypeCalc record, string title = null) {
			FullReportRecord rec=new FullReportRecord();
			rec.Title = String.IsNullOrEmpty(title) ? record.Title : title;
			rec.Key = record.ID;
			rec.Selectable = true;
			rec.Children = new List<FullReportRecord>();
			return rec;
		}

		public static FullReportRecord GetFullReportRecord(PiramidaRecord record, string title = null) {
			FullReportRecord rec=new FullReportRecord();
			rec.Title = String.IsNullOrEmpty(title) ? record.Title : title;
			rec.Key = record.Key;
			rec.Selectable = true;
			rec.Children = new List<FullReportRecord>();
			return rec;
		}

		
	}
}
