using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace VotGES.Piramida.Report
{
	public class FullReportRoot
	{
		public FullReportRecord RootMain { get; set; }
		public FullReportRecord RootLines { get; set; }
		public FullReportRecord RootSN { get; set; }
	}

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
		public FullReportRecord RootMain { get; set; }
		public FullReportRecord RootLines { get; set; }
		public FullReportRecord RootSN { get; set; }
		public FullReportInitData() {
			CreateMain();
			CreateLines();
			CreateSN();
		}


		protected void CreateMain() {
			FullReportRecord record,childRecord;
			RootMain = GetFullReportRecord("Воткинская ГЭС", "votGES");
			record = RootMain.addChild(GetFullReportRecord("Основные параметры", "mainParams"));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_GES));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_GTP1));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_GTP2));

			record = RootMain.addChild(GetFullReportRecord("Вода", "water"));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_Napor));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_NB));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_QGES));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_Temp));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_VB));

			record = RootMain.addChild(GetFullReportRecord("Генераторы", "generators"));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA1_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA1));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA2_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA2));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA3));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA3_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA3_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA3));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA4));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA4_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA4_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA4));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA5));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA5_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA5_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA5));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA6));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA1_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA6));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA7));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA7_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA7_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA7));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA8));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA8_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA8_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA8));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA9));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA9_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA9_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA9));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA10));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA10_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA10_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA10));

			
		}

		protected void CreateLines() {
			FullReportRecord record,childRecord, child2;
			RootLines = GetFullReportRecord("ВЛ", "VL");
			
			record = RootLines.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Saldo));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Priem));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Berezovka));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Berezovka_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Berezovka_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Dubovaya));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Dubovaya_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Dubovaya_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Ivanovka));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Ivanovka_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Ivanovka_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Kauchuk));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Kauchuk_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Kauchuk_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_KSHT1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_KSHT1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_KSHT1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_KSHT2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_KSHT2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_KSHT2_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Svetlaya));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Svetlaya_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Svetlaya_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_TEC));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_TEC_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_TEC_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Vodozabor1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Vodozabor1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Vodozabor1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Vodozabor2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Vodozabor2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Vodozabor2_Priem));

			record = RootLines.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Saldo));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Priem));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Izhevsk1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Izhevsk1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Izhevsk1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Izhevsk2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Izhevsk2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Izhevsk2_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Kauchuk1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Kauchuk1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Kauchuk1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Kauchuk2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Kauchuk2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Kauchuk2_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Svetlaya));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Svetlaya_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Svetlaya_Priem));

			record = RootLines.addChild(GetFullReportRecord(ReportLinesRecords.P_VL500_Saldo));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL500_Priem));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL500_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL500_Karmanovo));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL500_Karmanovo_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL500_Karmanovo_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL500_Emelino));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL500_Emelino_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL500_Emelino_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL500_Vyatka));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL500_Vyatka_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL500_Vyatka_Priem));

			record = RootLines.addChild(GetFullReportRecord("Главные трансформаторы", "mainTrans"));
			childRecord = record.addChild(GetFullReportRecord("1Т", "1T"));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_1T_110));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_56AT_110_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_56AT_110_Priem));

			childRecord = record.addChild(GetFullReportRecord("2АТ", "2AT"));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_2AT_220));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_2AT_220_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_2AT_220_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_2AT_500));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_2AT_500_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_2AT_500_Priem));

			childRecord = record.addChild(GetFullReportRecord("3АТ", "3AT"));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_3AT_220));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_3AT_220_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_3AT_220_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_3AT_500));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_3AT_500_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_3AT_500_Priem));

			childRecord = record.addChild(GetFullReportRecord("4Т", "4T"));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_4T_220));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_4T_220_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_4T_220_Priem));

			childRecord = record.addChild(GetFullReportRecord("5-6АТ", "56AT"));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_56AT_220));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_56AT_220_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_56AT_220_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_56AT_110));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_56AT_110_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_56AT_110_Priem));			
		}

		protected void CreateSN() {
			FullReportRecord record,childRecord, child2;
			RootSN = GetFullReportRecord("СН", "SN");


			record = RootSN.addChild(GetFullReportRecord(ReportMainRecords.P_SP));
			record.addChild(GetFullReportRecord(ReportGARecords.P_Vozb));
			record.addChild(GetFullReportRecord(ReportGARecords.P_SN_GA));
			record.addChild(GetFullReportRecord(ReportGARecords.P_SK));
			childRecord = record.addChild(GetFullReportRecord(ReportMainRecords.P_Nebalans));

			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_T_Nebalans));
			child2.addChild(GetFullReportRecord(ReportGlTransformRecords.P_1T_Nebalans));
			child2.addChild(GetFullReportRecord(ReportGlTransformRecords.P_2AT_Nebalans));
			child2.addChild(GetFullReportRecord(ReportGlTransformRecords.P_3AT_Nebalans));
			child2.addChild(GetFullReportRecord(ReportGlTransformRecords.P_4T_Nebalans));
			child2.addChild(GetFullReportRecord(ReportGlTransformRecords.P_56AT_Nebalans));

			child2 = childRecord.addChild(GetFullReportRecord(ReportLinesRecords.P_VL_Nebalans));
			child2.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Nebalans));
			child2.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Nebalans));
			child2.addChild(GetFullReportRecord(ReportLinesRecords.P_VL500_Nebalans));

			childRecord = record.addChild(GetFullReportRecord(ReportSNRecords.P_SN));

			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_7T_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_8T_Priem));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_1N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_21T_Priem));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_22T_Priem));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_2N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_23T_Priem));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_24T_Priem));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_3N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_27T_Priem));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_28T_Priem));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_7N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_1KU_31T_Priem));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_32T_Priem));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_8N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_25T_37T_Priem));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_38T_Priem));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_9N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_29T_Priem));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_30T_31T_Priem));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_10N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_33T_Priem));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_34T_Priem));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_36N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_35T_Priem));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_36T_Priem));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_Nasos));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_20NDS1_Priem));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_SN_20NDS2_Priem));

			child2 = childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_TVI_Priem));

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
