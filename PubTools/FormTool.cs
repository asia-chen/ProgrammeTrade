﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data;
using DevExpress.XtraCharts;

namespace PubTools
{
    public class FormTool
    {
        // 禁止Form中全部按钮
        public static void DisableButtons(Form f)
        {
            foreach (Control ctl in f.Controls)
            {
                if (ctl is Button)
                {
                    Button btn = ctl as Button;
                    btn.Enabled = false;
                }
            }
        }

        // 允许Form中全部按钮
        public static void EnableButtons(Form f)
        {
            foreach (Control ctl in f.Controls)
            {
                if (ctl is Button)
                {
                    Button btn = ctl as Button;
                    btn.Enabled = true;
                }
            }
        }

        // 根据交易时段设置label
        public static void SetCustomLabel(ChartControl chartControl, DataRow[] drSegment)
        {
            XYDiagram diagram = (XYDiagram)chartControl.Diagram;
            diagram.AxisX.WholeRange.SetMinMaxValues((String)drSegment[0]["BeginTime"], (String)drSegment[drSegment.Length-1]["EndTime"]);
            diagram.AxisX.VisualRange.SetMinMaxValues((String)drSegment[0]["BeginTime"], (String)drSegment[drSegment.Length - 1]["EndTime"]);

            String dt = DateTime.Now.ToString("yyyyMMdd");
            
            DateTime dtCur = DateTime.ParseExact(dt + (String)drSegment[0]["BeginTime"], "yyyyMMddHHmm", System.Globalization.CultureInfo.CurrentCulture);
            dtCur = dtCur.AddDays((long)drSegment[0]["BeginDay"]);

            CustomAxisLabelCollection labels = diagram.AxisX.CustomLabels;
            for (int i = 0; i < drSegment.Length; i++)
            {
                DateTime dtBegin = DateTime.ParseExact(dt + (String)drSegment[i]["BeginTime"], "yyyyMMddHHmm", System.Globalization.CultureInfo.CurrentCulture);
                dtBegin = dtBegin.AddDays((long)drSegment[i]["BeginDay"]);                                
                DateTime dtEnd = DateTime.ParseExact(dt + (String)drSegment[i]["EndTime"], "yyyyMMddHHmm", System.Globalization.CultureInfo.CurrentCulture);
                dtEnd = dtEnd.AddDays((long)drSegment[i]["EndDay"]);

                if (dtCur < dtBegin)
                {
                    if (i == 0)
                    {
                        dtCur = dtBegin;
                    }
                    else if (dtBegin.Hour == 10)
                    {
                        dtCur = dtBegin.AddMinutes(15);
                    }
                    else
                    {
                        dtCur = dtBegin.AddMinutes(30);
                    }
                }

                while (dtCur <= dtEnd)
                {
                    String strLabel = dtCur.ToString("HH:mm");
                    CustomAxisLabel onelabel = new CustomAxisLabel();
                    onelabel.AxisValue = strLabel;
                    onelabel.Name = strLabel;
                    labels.Add(onelabel);

                    dtCur = dtCur.AddMinutes(30);
                }
            }
        }

    }
}