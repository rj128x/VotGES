using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida
{
	public static class PiramidaRecords
	{
		public static PiramidaRecord P_GES=new PiramidaRecord(2, 0, 1, "P ГЭС");
		public static PiramidaRecord P_GTP1=new PiramidaRecord(2, 0, 2, "P ГТП1");
		public static PiramidaRecord P_GTP2=new PiramidaRecord(2, 0, 3, "P ГТП2");

		public static PiramidaRecord P_3AT_500_Priem=new PiramidaRecord(0, 8739, 1, "3АТ 500 кВ Прием (P)");
		public static PiramidaRecord P_3AT_500_Otd=new PiramidaRecord(0, 8739, 2, "3АТ 500 кВ Отдача (P)");
		public static PiramidaRecord Q_3AT_500_Priem=new PiramidaRecord(0, 8739, 3, "3АТ 500 кВ Прием (Q)");
		public static PiramidaRecord Q_3AT_500_Otd=new PiramidaRecord(0, 8739, 4, "3АТ 500 кВ Отдача (Q)");

		public static PiramidaRecord P_2AT_500_Priem=new PiramidaRecord(0, 8739, 5, "2АТ 500 кВ Прием (P)");
		public static PiramidaRecord P_2AT_500_Otd=new PiramidaRecord(0, 8739, 6, "2АТ 500 кВ Отдача (P)");
		public static PiramidaRecord Q_2AT_500_Priem=new PiramidaRecord(0, 8739, 7, "2АТ 500 кВ Прием (Q)");
		public static PiramidaRecord Q_2AT_500_Otd=new PiramidaRecord(0, 8739, 8, "2АТ 500 кВ Отдача (Q)");

		public static PiramidaRecord P_20NDS1_Priem=new PiramidaRecord(0, 8739, 9, "20НДС-1 прием (P)");
		public static PiramidaRecord Q_20NDS1_Priem=new PiramidaRecord(0, 8739, 10, "20НДС-1 прием (Q)");
		public static PiramidaRecord P_1KU_K2_Priem=new PiramidaRecord(0, 8739, 11, "Компрессор-2 1КУ прием (P)");
		public static PiramidaRecord Q_1KU_K2_Priem=new PiramidaRecord(0, 8739, 12, "Компрессор-2 1КУ прием (Q)");
		public static PiramidaRecord P_1KU_31T_Priem=new PiramidaRecord(0, 8739, 13, "31Т 1КУ прием (P)");
		public static PiramidaRecord Q_1KU_31T_Priem=new PiramidaRecord(0, 8739, 14, "31Т 1КУ прием (Q)");


		public static PiramidaRecord P_29T_Priem=new PiramidaRecord(0, 8739, 15, "29Т прием (P)");
		public static PiramidaRecord P_27T_Priem=new PiramidaRecord(0, 8739, 17, "27Т прием (P)");
		public static PiramidaRecord P_22T_Priem=new PiramidaRecord(0, 8739, 18, "22Т прием (P)");
		public static PiramidaRecord P_30T_Priem=new PiramidaRecord(0, 8739, 19, "30Т прием (P)");
		public static PiramidaRecord P_25T_Priem=new PiramidaRecord(0, 8739, 20, "25Т прием (P)");
		public static PiramidaRecord P_33T_Priem=new PiramidaRecord(0, 8739, 21, "33Т прием (P)");
		public static PiramidaRecord P_21T_Priem=new PiramidaRecord(0, 8739, 22, "21Т прием (P)");
		public static PiramidaRecord P_34T_Priem=new PiramidaRecord(0, 8739, 23, "34Т прием (P)");

		public static PiramidaRecord P_1KU_K5_Priem=new PiramidaRecord(0, 8739, 25, "Компрессор-5 1КУ прием (P)");
		public static PiramidaRecord Q_1KU_K5_Priem=new PiramidaRecord(0, 8739, 26, "Компрессор-5 1КУ прием (Q)");

		public static PiramidaRecord P_32T_Priem=new PiramidaRecord(0, 8739, 27, "32Т прием (P)");

		public static PiramidaRecord P_20NDS2_Priem=new PiramidaRecord(0, 8739, 28, "20НДС-2 прием (P)");
		public static PiramidaRecord Q_20NDS2_Priem=new PiramidaRecord(0, 8739, 29, "20НДС-2 прием (Q)");

		public static PiramidaRecord P_23T_Priem=new PiramidaRecord(0, 8739, 30, "23Т прием (P)");
		public static PiramidaRecord P_28T_Priem=new PiramidaRecord(0, 8739, 31, "28Т прием (P)");
		public static PiramidaRecord P_35T_Priem=new PiramidaRecord(0, 8739, 32, "35Т прием (P)");
		public static PiramidaRecord P_36T_Priem=new PiramidaRecord(0, 8739, 33, "36Т прием (P)");
		public static PiramidaRecord P_26T_Priem=new PiramidaRecord(0, 8739, 34, "26Т прием (P)");

		public static PiramidaRecord P_TVI_Priem=new PiramidaRecord(0, 8739, 35, "ТВИ прием (P)");
		public static PiramidaRecord Q_TVI_Priem=new PiramidaRecord(0, 8739, 36, "ТВИ прием (Q)");

		public static PiramidaRecord P_24T_Priem=new PiramidaRecord(0, 8739, 37, "24Т прием (P)");

		public static PiramidaRecord P_Vozb_GA9_Priem=new PiramidaRecord(0, 8740, 1, "Возбуждение Г/А 9 прием (P)");
		public static PiramidaRecord P_Vozb_GA10_Priem=new PiramidaRecord(0, 8740, 2, "Возбуждение Г/А 10 прием (P)");
		public static PiramidaRecord P_SN_19T_Priem=new PiramidaRecord(0, 8740, 3, "СН 19Т прием (P)");
		public static PiramidaRecord P_SN_20T_Priem=new PiramidaRecord(0, 8740, 4, "СН 20Т прием (P)");
		public static PiramidaRecord P_Vozb_GA7_Priem=new PiramidaRecord(0, 8740, 5, "Возбуждение Г/А 7 прием (P)");
		public static PiramidaRecord P_Vozb_GA8_Priem=new PiramidaRecord(0, 8740, 6, "Возбуждение Г/А 8 прием (P)");
		public static PiramidaRecord P_SN_17T_Priem=new PiramidaRecord(0, 8740, 7, "СН 17Т прием (P)");
		public static PiramidaRecord P_SN_18T_Priem=new PiramidaRecord(0, 8740, 8, "СН 28Т прием (P)");

		public static PiramidaRecord P_1T_110_Priem=new PiramidaRecord(0, 8740, 9, "1Т 110 кВ Прием (P)");
		public static PiramidaRecord P_1T_110_Otd=new PiramidaRecord(0, 8740, 10, "1Т 110 кВ Отдача (P)");
		public static PiramidaRecord Q_1T_110_Priem=new PiramidaRecord(0, 8740, 11, "1Т 110 кВ Прием (Q)");
		public static PiramidaRecord Q_1T_110_Otd=new PiramidaRecord(0, 8740, 12, "1Т 110 кВ Отдача (Q)");

		public static PiramidaRecord P_2AT_220_Priem=new PiramidaRecord(0, 8740, 13, "2АТ 220 кВ Прием (P)");
		public static PiramidaRecord P_2AT_220_Otd=new PiramidaRecord(0, 8740, 14, "2АТ 220 кВ Отдача (P)");
		public static PiramidaRecord Q_2AT_220_Priem=new PiramidaRecord(0, 8740, 15, "2АТ 220 кВ Прием (Q)");
		public static PiramidaRecord Q_2AT_220_Otd=new PiramidaRecord(0, 8740, 16, "2АТ 220 кВ Отдача (Q)");

		public static PiramidaRecord P_3AT_220_Priem=new PiramidaRecord(0, 8740, 17, "3АТ 220 кВ Прием (P)");
		public static PiramidaRecord P_3AT_220_Otd=new PiramidaRecord(0, 8740, 18, "3АТ 220 кВ Отдача (P)");
		public static PiramidaRecord Q_3AT_220_Priem=new PiramidaRecord(0, 8740, 19, "3АТ 220 кВ Прием (Q)");
		public static PiramidaRecord Q_3AT_220_Otd=new PiramidaRecord(0, 8740, 20, "3АТ 220 кВ Отдача (Q)");

		public static PiramidaRecord P_56AT_220_Priem=new PiramidaRecord(0, 8740, 21, "5-6АТ 220 кВ Прием (P)");
		public static PiramidaRecord P_56AT_220_Otd=new PiramidaRecord(0, 8740, 22, "5-6АТ 220 кВ Отдача (P)");
		public static PiramidaRecord Q_56AT_220_Priem=new PiramidaRecord(0, 8740, 23, "5-6АТ 220 кВ Прием (Q)");
		public static PiramidaRecord Q_56AT_220_Otd=new PiramidaRecord(0, 8740, 24, "5-6АТ 220 кВ Отдача (Q)");

		public static PiramidaRecord P_4T_220_Priem=new PiramidaRecord(0, 8740, 25, "4Т 220 кВ Прием (P)");
		public static PiramidaRecord P_4T_220_Otd=new PiramidaRecord(0, 8740, 26, "4Т 220 кВ Отдача (P)");
		public static PiramidaRecord Q_4T_220_Priem=new PiramidaRecord(0, 8740, 27, "4Т 220 кВ Прием (Q)");
		public static PiramidaRecord Q_4T_220_Otd=new PiramidaRecord(0, 8740, 28, "4Т 220 кВ Отдача (Q)");

		public static PiramidaRecord P_Vozb_GA5_Priem=new PiramidaRecord(0, 8740, 29, "Возбуждение Г/А 5 прием (P)");
		public static PiramidaRecord P_Vozb_GA6_Priem=new PiramidaRecord(0, 8740, 30, "Возбуждение Г/А 6 прием (P)");
		public static PiramidaRecord P_SN_15T_Priem=new PiramidaRecord(0, 8740, 31, "СН 15Т прием (P)");
		public static PiramidaRecord P_SN_16T_Priem=new PiramidaRecord(0, 8740, 32, "СН 26Т прием (P)");
		public static PiramidaRecord P_SN_8T_Priem=new PiramidaRecord(0, 8740, 33, "СН 8Т прием (P)");

		public static PiramidaRecord P_Vozb_GA1_Priem=new PiramidaRecord(0, 8740, 34, "Возбуждение Г/А 1 прием (P)");
		public static PiramidaRecord P_Vozb_GA2_Priem=new PiramidaRecord(0, 8740, 35, "Возбуждение Г/А 2 прием (P)");
		public static PiramidaRecord P_SN_11T_Priem=new PiramidaRecord(0, 8740, 36, "СН 11Т прием (P)");
		public static PiramidaRecord P_SN_12T_Priem=new PiramidaRecord(0, 8740, 37, "СН 12Т прием (P)");
		public static PiramidaRecord P_SN_7T_Priem=new PiramidaRecord(0, 8740, 38, "СН 7Т прием (P)");

		public static PiramidaRecord P_Vozb_GA3_Priem=new PiramidaRecord(0, 8740, 39, "Возбуждение Г/А 3 прием (P)");
		public static PiramidaRecord P_Vozb_GA4_Priem=new PiramidaRecord(0, 8740, 40, "Возбуждение Г/А 4 прием (P)");
		public static PiramidaRecord P_SN_13T_Priem=new PiramidaRecord(0, 8740, 41, "СН 13Т прием (P)");
		public static PiramidaRecord P_SN_14T_Priem=new PiramidaRecord(0, 8740, 42, "СН 14Т прием (P)");

		public static PiramidaRecord P_56AT_110_Priem=new PiramidaRecord(0, 8740, 43, "5-6АТ 110 кВ Прием (P)");
		public static PiramidaRecord P_56AT_110_Otd=new PiramidaRecord(0, 8740, 44, "5-6АТ 110 кВ Отдача (P)");
		public static PiramidaRecord Q_56AT_110_Priem=new PiramidaRecord(0, 8740, 45, "5-6АТ 110 кВ Прием (Q)");
		public static PiramidaRecord Q_56AT_110_Otd=new PiramidaRecord(0, 8740, 46, "5-6АТ 110 кВ Отдача (Q)");

		public static PiramidaRecord P_VL110_Svetlaya_Priem=new PiramidaRecord(0, 8737, 1, "ВЛ 110 Светлая прием (P)");
		public static PiramidaRecord P_VL110_Svetlaya_Otd=new PiramidaRecord(0, 8737, 2, "ВЛ 110 Светлая отдача (P)");
		public static PiramidaRecord Q_VL110_Svetlaya_Priem=new PiramidaRecord(0, 8737, 3, "ВЛ 110 Светлая прием (Q)");
		public static PiramidaRecord Q_VL110_Svetlaya_Otd=new PiramidaRecord(0, 8737, 4, "ВЛ 110 Светлая отдача (Q)");

		public static PiramidaRecord P_VL110_Ivanovka_Priem=new PiramidaRecord(0, 8737, 5, "ВЛ 110 Ивановка прием (P)");
		public static PiramidaRecord P_VL110_Ivanovka_Otd=new PiramidaRecord(0, 8737, 6, "ВЛ 110 Ивановка отдача (P)");
		public static PiramidaRecord Q_VL110_Ivanovka_Priem=new PiramidaRecord(0, 8737, 7, "ВЛ 110 Ивановка прием (Q)");
		public static PiramidaRecord Q_VL110_Ivanovka_Otd=new PiramidaRecord(0, 8737, 8, "ВЛ 110 Ивановка отдача (Q)");

		public static PiramidaRecord P_VL110_Kauchuk_Priem=new PiramidaRecord(0, 8737, 9, "ВЛ 110 Каучук прием (P)");
		public static PiramidaRecord P_VL110_Kauchuk_Otd=new PiramidaRecord(0, 8737, 10, "ВЛ 110 Каучук отдача (P)");
		public static PiramidaRecord Q_VL110_Kauchuk_Priem=new PiramidaRecord(0, 8737, 11, "ВЛ 110 Каучук прием (Q)");
		public static PiramidaRecord Q_VL110_Kauchuk_Otd=new PiramidaRecord(0, 8737, 12, "ВЛ 110 Каучук отдача (Q)");

		public static PiramidaRecord P_VL110_TEC_Priem=new PiramidaRecord(0, 8737, 13, "ВЛ 110 ЧаТЭЦ прием (P)");
		public static PiramidaRecord P_VL110_TEC_Otd=new PiramidaRecord(0, 8737, 14, "ВЛ 110 ЧаТЭЦ отдача (P)");
		public static PiramidaRecord Q_VL110_TEC_Priem=new PiramidaRecord(0, 8737, 15, "ВЛ 110 ЧаТЭЦ прием (Q)");
		public static PiramidaRecord Q_VL110_TEC_Otd=new PiramidaRecord(0, 8737, 16, "ВЛ 110 ЧаТЭЦ отдача (Q)");

		public static PiramidaRecord P_VL110_Berezovka_Priem=new PiramidaRecord(0, 8737, 17, "ВЛ 110 Березовка прием (P)");
		public static PiramidaRecord P_VL110_Berezovka_Otd=new PiramidaRecord(0, 8737, 18, "ВЛ 110 Березовка отдача (P)");
		public static PiramidaRecord Q_VL110_Berezovka_Priem=new PiramidaRecord(0, 8737, 19, "ВЛ 110 Березовка прием (Q)");
		public static PiramidaRecord Q_VL110_Berezovka_Otd=new PiramidaRecord(0, 8737, 20, "ВЛ 110 Березовка отдача (Q)");

		public static PiramidaRecord P_VL220_Svetlaya_Priem=new PiramidaRecord(0, 8737, 21, "ВЛ 220 Светлая прием (P)");
		public static PiramidaRecord P_VL220_Svetlaya_Otd=new PiramidaRecord(0, 8737, 22, "ВЛ 220 Светлая отдача (P)");
		public static PiramidaRecord Q_VL220_Svetlaya_Priem=new PiramidaRecord(0, 8737, 23, "ВЛ 220 Светлая прием (Q)");
		public static PiramidaRecord Q_VL220_Svetlaya_Otd=new PiramidaRecord(0, 8737, 24, "ВЛ 220 Светлая отдача (Q)");

		public static PiramidaRecord P_VL220_Kauchuk1_Priem=new PiramidaRecord(0, 8737, 25, "ВЛ 220 Каучук-1 прием (P)");
		public static PiramidaRecord P_VL220_Kauchuk1_Otd=new PiramidaRecord(0, 8737, 26, "ВЛ 220 Каучук-1 отдача (P)");
		public static PiramidaRecord Q_VL220_Kauchuk1_Priem=new PiramidaRecord(0, 8737, 27, "ВЛ 220 Каучук-1 прием (Q)");
		public static PiramidaRecord Q_VL220_Kauchuk1_Otd=new PiramidaRecord(0, 8737, 28, "ВЛ 220 Каучук-1 отдача (Q)");

		public static PiramidaRecord P_VL220_Kauchuk2_Priem=new PiramidaRecord(0, 8737, 29, "ВЛ 220 Каучук-2 прием (P)");
		public static PiramidaRecord P_VL220_Kauchuk2_Otd=new PiramidaRecord(0, 8737, 30, "ВЛ 220 Каучук-2 отдача (P)");
		public static PiramidaRecord Q_VL220_Kauchuk2_Priem=new PiramidaRecord(0, 8737, 31, "ВЛ 220 Каучук-2 прием (Q)");
		public static PiramidaRecord Q_VL220_Kauchuk2_Otd=new PiramidaRecord(0, 8737, 32, "ВЛ 220 Каучук-2 отдача (Q)");

		public static PiramidaRecord P_VL220_Izhevsk1_Priem=new PiramidaRecord(0, 8737, 33, "ВЛ 220 Ижевск-1 прием (P)");
		public static PiramidaRecord P_VL220_Izhevsk1_Otd=new PiramidaRecord(0, 8737, 34, "ВЛ 220 Ижевск-1 отдача (P)");
		public static PiramidaRecord Q_VL220_Izhevsk1_Priem=new PiramidaRecord(0, 8737, 35, "ВЛ 220 Ижевск-1 прием (Q)");
		public static PiramidaRecord Q_VL220_Izhevsk1_Otd=new PiramidaRecord(0, 8737, 36, "ВЛ 220 Ижевск-1 отдача (Q)");

		public static PiramidaRecord P_VL220_Izhevsk2_Priem=new PiramidaRecord(0, 8737, 37, "ВЛ 220 Ижевск-2 прием (P)");
		public static PiramidaRecord P_VL220_Izhevsk2_Otd=new PiramidaRecord(0, 8737, 38, "ВЛ 220 Ижевск-2 отдача (P)");
		public static PiramidaRecord Q_VL220_Izhevsk2_Priem=new PiramidaRecord(0, 8737, 39, "ВЛ 220 Ижевск-2 прием (Q)");
		public static PiramidaRecord Q_VL220_Izhevsk2_Otd=new PiramidaRecord(0, 8737, 40, "ВЛ 220 Ижевск-2 отдача (Q)");

		public static PiramidaRecord P_VL110_KSHT1_Priem=new PiramidaRecord(0, 8737, 41, "ВЛ 110 КШТ-1 прием (P)");
		public static PiramidaRecord P_VL110_KSHT1_Otd=new PiramidaRecord(0, 8737, 42, "ВЛ 110 КШТ-1 отдача (P)");
		public static PiramidaRecord Q_VL110_KSHT1_Priem=new PiramidaRecord(0, 8737, 43, "ВЛ 110 КШТ-1 прием (Q)");
		public static PiramidaRecord Q_VL110_KSHT1_Otd=new PiramidaRecord(0, 8737, 44, "ВЛ 110 КШТ-1 отдача (Q)");

		public static PiramidaRecord P_VL110_KSHT2_Priem=new PiramidaRecord(0, 8737, 45, "ВЛ 110 КШТ-2 прием (P)");
		public static PiramidaRecord P_VL110_KSHT2_Otd=new PiramidaRecord(0, 8737, 46, "ВЛ 110 КШТ-2 отдача (P)");
		public static PiramidaRecord Q_VL110_KSHT2_Priem=new PiramidaRecord(0, 8737, 47, "ВЛ 110 КШТ-2 прием (Q)");
		public static PiramidaRecord Q_VL110_KSHT2_Otd=new PiramidaRecord(0, 8737, 48, "ВЛ 110 КШТ-2 отдача (Q)");

		public static PiramidaRecord P_VL110_Dubovaya_Priem=new PiramidaRecord(0, 8737, 49, "ВЛ 110 Дубовая прием (P)");
		public static PiramidaRecord P_VL110_Dubovaya_Otd=new PiramidaRecord(0, 8737, 50, "ВЛ 110 Дубовая отдача (P)");
		public static PiramidaRecord Q_VL110_Dubovaya_Priem=new PiramidaRecord(0, 8737, 51, "ВЛ 110 Дубовая прием (Q)");
		public static PiramidaRecord Q_VL110_Dubovaya_Otd=new PiramidaRecord(0, 8737, 52, "ВЛ 110 Дубовая отдача (Q)");

		public static PiramidaRecord P_VL110_Vodozabor2_Priem=new PiramidaRecord(0, 8737, 53, "ВЛ 110 Водозабор-2 прием (P)");
		public static PiramidaRecord P_VL110_Vodozabor2_Otd=new PiramidaRecord(0, 8737, 54, "ВЛ 110 Водозабор-2 отдача (P)");
		public static PiramidaRecord Q_VL110_Vodozabor2_Priem=new PiramidaRecord(0, 8737, 55, "ВЛ 110 Водозабор-2 прием (Q)");
		public static PiramidaRecord Q_VL110_Vodozabor2_Otd=new PiramidaRecord(0, 8737, 56, "ВЛ 110 Водозабор-2 отдача (Q)");

		public static PiramidaRecord P_VL110_Vodozabor1_Priem=new PiramidaRecord(0, 8737, 57, "ВЛ 110 Водозабор-1 прием (P)");
		public static PiramidaRecord P_VL110_Vodozabor1_Otd=new PiramidaRecord(0, 8737, 58, "ВЛ 110 Водозабор-1 отдача (P)");
		public static PiramidaRecord Q_VL110_Vodozabor1_Priem=new PiramidaRecord(0, 8737, 59, "ВЛ 110 Водозабор-1 прием (Q)");
		public static PiramidaRecord Q_VL110_Vodozabor1_Otd=new PiramidaRecord(0, 8737, 60, "ВЛ 110 Водозабор-1 отдача (Q)");

		public static PiramidaRecord P_VL500_Emelino_Priem=new PiramidaRecord(0, 8737, 61, "ВЛ 500 Емелино прием (P)");
		public static PiramidaRecord P_VL500_Emelino_Otd=new PiramidaRecord(0, 8737, 62, "ВЛ 500 Емелино отдача (P)");
		public static PiramidaRecord Q_VL500_Emelino_Priem=new PiramidaRecord(0, 8737, 63, "ВЛ 500 Емелино прием (Q)");
		public static PiramidaRecord Q_VL500_Emelino_Otd=new PiramidaRecord(0, 8737, 64, "ВЛ 500 Емелино отдача (Q)");

		public static PiramidaRecord P_VL500_Karmanovo_Priem=new PiramidaRecord(0, 8737, 65, "ВЛ 500 Карманово прием (P)");
		public static PiramidaRecord P_VL500_Karmanovo_Otd=new PiramidaRecord(0, 8737, 66, "ВЛ 500 Карманово отдача (P)");
		public static PiramidaRecord Q_VL500_Karmanovo_Priem=new PiramidaRecord(0, 8737, 67, "ВЛ 500 Карманово прием (Q)");
		public static PiramidaRecord Q_VL500_Karmanovo_Otd=new PiramidaRecord(0, 8737, 68, "ВЛ 500 Карманово отдача (Q)");

		public static PiramidaRecord P_VL500_Vytka_Priem=new PiramidaRecord(0, 8737, 69, "ВЛ 500 Вятка прием (P)");
		public static PiramidaRecord P_VL500_Vyatka_Otd=new PiramidaRecord(0, 8737, 70, "ВЛ 500 Вятка отдача (P)");
		public static PiramidaRecord Q_VL500_Vyatka_Priem=new PiramidaRecord(0, 8737, 71, "ВЛ 500 Вятка прием (Q)");
		public static PiramidaRecord Q_VL500_Vyatka_Otd=new PiramidaRecord(0, 8737, 72, "ВЛ 500 Вятка отдача (Q)");

		public static PiramidaRecord P_GA1_Priem=new PiramidaRecord(0, 8738, 1, "Генератор-1 прием (P)");
		public static PiramidaRecord P_GA1_Otd=new PiramidaRecord(0, 8738, 2, "Генератор-1 отдача (P)");
		public static PiramidaRecord Q_GA1_Priem=new PiramidaRecord(0, 8738, 3, "Генератор-1 прием (Q)");
		public static PiramidaRecord Q_GA1_Otd=new PiramidaRecord(0, 8738, 4, "Генератор-1 отдача (Q)");

		public static PiramidaRecord P_GA2_Priem=new PiramidaRecord(0, 8738, 5, "Генератор-2 прием (P)");
		public static PiramidaRecord P_GA2_Otd=new PiramidaRecord(0, 8738, 6, "Генератор-2 отдача (P)");
		public static PiramidaRecord Q_GA2_Priem=new PiramidaRecord(0, 8738, 7, "Генератор-2 прием (Q)");
		public static PiramidaRecord Q_GA2_Otd=new PiramidaRecord(0, 8738, 8, "Генератор-2 отдача (Q)");

		public static PiramidaRecord P_KL6_Shluz1_Priem=new PiramidaRecord(0, 8738, 9, "КЛ 6 Шлюз-1 прием (P)");
		public static PiramidaRecord P_KL6_Shluz1_Otd=new PiramidaRecord(0, 8738, 10, "КЛ 6 Шлюз-1 отдача (P)");
		public static PiramidaRecord Q_KL6_Shluz1_Priem=new PiramidaRecord(0, 8738, 11, "КЛ 6 Шлюз-1 прием (Q)");
		public static PiramidaRecord Q_KL6_Shluz1_Otd=new PiramidaRecord(0, 8738, 12, "КЛ 6 Шлюз-1 отдача (Q)");
		public static PiramidaRecord P_KL6_Shluz2_Priem=new PiramidaRecord(0, 8738, 13, "КЛ 6 Шлюз-2 прием (P)");
		public static PiramidaRecord P_KL6_Shluz2_Otd=new PiramidaRecord(0, 8738, 14, "КЛ 6 Шлюз-2 отдача (P)");
		public static PiramidaRecord Q_KL6_Shluz2_Priem=new PiramidaRecord(0, 8738, 15, "КЛ 6 Шлюз-2 прием (Q)");
		public static PiramidaRecord Q_KL6_Shluz2_Otd=new PiramidaRecord(0, 8738, 16, "КЛ 6 Шлюз-2 отдача (Q)");

		public static PiramidaRecord P_GA3_Priem=new PiramidaRecord(0, 8738, 17, "Генератор-3 прием (P)");
		public static PiramidaRecord P_GA3_Otd=new PiramidaRecord(0, 8738, 18, "Генератор-3 отдача (P)");
		public static PiramidaRecord Q_GA3_Priem=new PiramidaRecord(0, 8738, 18, "Генератор-3 прием (Q)");
		public static PiramidaRecord Q_GA3_Otd=new PiramidaRecord(0, 8738, 20, "Генератор-3 отдача (Q)");

		public static PiramidaRecord P_GA4_Priem=new PiramidaRecord(0, 8738, 21, "Генератор-4 прием (P)");
		public static PiramidaRecord P_GA4_Otd=new PiramidaRecord(0, 8738, 22, "Генератор-4 отдача (P)");
		public static PiramidaRecord Q_GA4_Priem=new PiramidaRecord(0, 8738, 23, "Генератор-4 прием (Q)");
		public static PiramidaRecord Q_GA4_Otd=new PiramidaRecord(0, 8738, 24, "Генератор-4 отдача (Q)");

		public static PiramidaRecord P_GA5_Priem=new PiramidaRecord(0, 8738, 25, "Генератор-5 прием (P)");
		public static PiramidaRecord P_GA5_Otd=new PiramidaRecord(0, 8738, 26, "Генератор-5 отдача (P)");
		public static PiramidaRecord Q_GA5_Priem=new PiramidaRecord(0, 8738, 27, "Генератор-5 прием (Q)");
		public static PiramidaRecord Q_GA5_Otd=new PiramidaRecord(0, 8738, 28, "Генератор-5 отдача (Q)");

		public static PiramidaRecord P_GA6_Priem=new PiramidaRecord(0, 8738, 29, "Генератор-6 прием (P)");
		public static PiramidaRecord P_GA6_Otd=new PiramidaRecord(0, 8738, 30, "Генератор-6 отдача (P)");
		public static PiramidaRecord Q_GA6_Priem=new PiramidaRecord(0, 8738, 31, "Генератор-6 прием (Q)");
		public static PiramidaRecord Q_GA6_Otd=new PiramidaRecord(0, 8738, 32, "Генератор-6 отдача (Q)");

		public static PiramidaRecord P_KL6_Filtr1_Priem=new PiramidaRecord(0, 8738, 33, "КЛ 6 Фильтр-1 прием (P)");
		public static PiramidaRecord P_KL6_Filtr1_Otd=new PiramidaRecord(0, 8738, 34, "КЛ 6 Фильтр-1 отдача (P)");
		public static PiramidaRecord Q_KL6_Filtr1_Priem=new PiramidaRecord(0, 8738, 35, "КЛ 6 Фильтр-1 прием (Q)");
		public static PiramidaRecord Q_KL6_Filtr1_Otd=new PiramidaRecord(0, 8738, 36, "КЛ 6 Фильтр-1 отдача (Q)");
		public static PiramidaRecord P_KL6_Filtr2_Priem=new PiramidaRecord(0, 8738, 37, "КЛ 6 Фильтр-2 прием (P)");
		public static PiramidaRecord P_KL6_Filtr2_Otd=new PiramidaRecord(0, 8738, 38, "КЛ 6 Фильтр-2 отдача (P)");
		public static PiramidaRecord Q_KL6_Filtr2_Priem=new PiramidaRecord(0, 8738, 39, "КЛ 6 Фильтр-2 прием (Q)");
		public static PiramidaRecord Q_KL6_Filtr2_Otd=new PiramidaRecord(0, 8738, 40, "КЛ 6 Фильтр-2 отдача (Q)");

		public static PiramidaRecord P_GA7_Priem=new PiramidaRecord(0, 8738, 41, "Генератор-7 прием (P)");
		public static PiramidaRecord P_GA7_Otd=new PiramidaRecord(0, 8738, 42, "Генератор-7 отдача (P)");
		public static PiramidaRecord Q_GA7_Priem=new PiramidaRecord(0, 8738, 43, "Генератор-7 прием (Q)");
		public static PiramidaRecord Q_GA7_Otd=new PiramidaRecord(0, 8738, 44, "Генератор-7 отдача (Q)");

		public static PiramidaRecord P_GA8_Priem=new PiramidaRecord(0, 8738, 45, "Генератор-8 прием (P)");
		public static PiramidaRecord P_GA8_Otd=new PiramidaRecord(0, 8738, 46, "Генератор-8 отдача (P)");
		public static PiramidaRecord Q_GA8_Priem=new PiramidaRecord(0, 8738, 47, "Генератор-8 прием (Q)");
		public static PiramidaRecord Q_GA8_Otd=new PiramidaRecord(0, 8738, 48, "Генератор-8 отдача (Q)");

		public static PiramidaRecord P_GA9_Priem=new PiramidaRecord(0, 8738, 49, "Генератор-9 прием (P)");
		public static PiramidaRecord P_GA9_Otd=new PiramidaRecord(0, 8738, 50, "Генератор-9 отдача (P)");
		public static PiramidaRecord Q_GA9_Priem=new PiramidaRecord(0, 8738, 51, "Генератор-9 прием (Q)");
		public static PiramidaRecord Q_GA9_Otd=new PiramidaRecord(0, 8738, 52, "Генератор-9 отдача (Q)");

		public static PiramidaRecord P_GA10_Priem=new PiramidaRecord(0, 8738, 53, "Генератор-10 прием (P)");
		public static PiramidaRecord P_GA10_Otd=new PiramidaRecord(0, 8738, 54, "Генератор-10 отдача (P)");
		public static PiramidaRecord Q_GA10_Priem=new PiramidaRecord(0, 8738, 55, "Генератор-10 прием (Q)");
		public static PiramidaRecord Q_GA10_Otd=new PiramidaRecord(0, 8738, 56, "Генератор-10 отдача (Q)");

		public static PiramidaRecord Water_NB=new PiramidaRecord(2, 1, 275, "НБ");
		public static PiramidaRecord Water_VB=new PiramidaRecord(2, 1, 274, "ВБ");
		public static PiramidaRecord Water_Napor=new PiramidaRecord(2, 1, 276, "Напор");
		public static PiramidaRecord Water_Temp=new PiramidaRecord(2, 1, 373, "Температура");
		public static PiramidaRecord Water_QGES=new PiramidaRecord(2, 1, 354, "Расход ГЭС");
		public static PiramidaRecord Water_QOptGES=new PiramidaRecord(2, 10, 3, "Расход ГЭС");

		public static PiramidaRecord Water_Q_GA1=new PiramidaRecord(2, 1, 104, "Расход ГА-1");
		public static PiramidaRecord Water_Q_GA2=new PiramidaRecord(2, 1, 129, "Расход ГА-2");
		public static PiramidaRecord Water_Q_GA3=new PiramidaRecord(2, 1, 154, "Расход ГА-3");
		public static PiramidaRecord Water_Q_GA4=new PiramidaRecord(2, 1, 179, "Расход ГА-4");
		public static PiramidaRecord Water_Q_GA5=new PiramidaRecord(2, 1, 204, "Расход ГА-5");
		public static PiramidaRecord Water_Q_GA6=new PiramidaRecord(2, 1, 229, "Расход ГА-6");
		public static PiramidaRecord Water_Q_GA7=new PiramidaRecord(2, 1, 254, "Расход ГА-7");
		public static PiramidaRecord Water_Q_GA8=new PiramidaRecord(2, 1, 279, "Расход ГА-8");
		public static PiramidaRecord Water_Q_GA9=new PiramidaRecord(2, 1, 304, "Расход ГА-9");
		public static PiramidaRecord Water_Q_GA10=new PiramidaRecord(2, 1, 329, "Расход ГА-10");

		static void PiramidaRecord(){

		}

		public static void addRecord(string key, int objType, int obj, int item, string title) {
		}
	}
}

