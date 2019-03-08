using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Components.Table
{
    //to do also can be extension from frontend

    public interface ICellType
    {

    }

    public class ArrayCell: ICellType
    {
        public List<ArrayItem> Items { get; set; }
        
        public string ArrayData { get; set; }

        public ArrayItem ArrayItemTemplate { get; set; }

        public string ArrayItemSyntax => "arrayItem";

        public ArrayCell(string arrayData,string dataType)
        {
            ArrayData = arrayData;
            //todo convert {modeldata.key} to right value
            ArrayItemTemplate = new ArrayItem("{arrayItem.value} Kooboo.text.component.table['{arrayItem.key}']", "label label-sm kb-table-label-refer",
                "background-color: Kooboo.getLabelColor('{arrayItem.key}')", "{id:{modeldata.id},by:{arrayItem.key}},type:'" + dataType + "'",
                "relationCallback(rel)");

        }
    }

    public class ArrayItem : ICellType
    {
        public string Text { get; set; }

        public string ClassName { get; set; }

        public string Style { get; set; }

        public string Data { get; set; }

        public string ClickEvent { get; set; }

       
        public ArrayItem(string text,string className,string style,string data,string clickEvent)
        {
            Text = text;
            ClassName = className;
            Style = style;
            Data = data;
            ClickEvent = clickEvent;
        }
    }

    public class BadgeCell : ICellType
    {
        public string Text { get; set; }

        public string ClassName { get; set; }

        public string ClickEvent { get; set; }

        public BadgeCell(string text,string className,string clickEvent)
        {
            Text = text;
            ClassName = className;
            ClickEvent = clickEvent;
        }

    }

    public class ButtonCell : ICellType
    {
        public string Text { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string ClassName { get; set; }

        public string Icon { get; set; }

        public bool InNewWindow { get; set; }

        public ButtonCell(string text,string title,string url,string className,string icon,bool inNewWindow)
        {
            Text = text;
            Title = title;
            Url = url;
            ClassName = className;
            Icon = icon;
            InNewWindow = inNewWindow;
        }
    }

    public class IconTextCell : ICellType
    {

        public string Title { get; set; }

        public string Text { get; set; }

        public string Icon { get; set; }//same to iconCell className

        public IconTextCell(string text, string title,string icon)
        {
            Title = title;
            Text = text;
            Icon = icon;
        }
    }

    public class IconCell : ICellType
    {
        public string ClassName { get; set;}

        public string Title { get; set; }

        public IconCell(string className,string title)
        {
            ClassName = className;
            Title = title;
        }
    }

    public class LabelCell : ICellType
    {
        public string Text { get; set; }

        public string Url { get; set; }

        public string ClassName { get; set; }

        public bool InNewWindow { get; set; }

        public string ClickEvent { get; set; }

        public LabelCell(string text,string url,string className,bool inNewWindow,string clickEvent)
        {
            Text = text;
            Url = url;
            ClassName = className;
            InNewWindow = inNewWindow;
            ClickEvent = clickEvent;

        }
    }

    public class LinkCell : ICellType
    {
        public string Text { get; set; }

        public string Url { get; set; }
        public LinkCell(string text,string url)
        {
            Text = text;
            Url = url;
        }
    }

    public class SummaryCell : ICellType
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }

        public bool InNewWindow { get; set; }

        public string ClickEvent { get; set; }

        public SummaryCell(string title,string text,string url,bool inNewWindow,string clickEvent)
        {
            Title = title;
            Text = text;
            Url = url;
            InNewWindow = inNewWindow;
            ClickEvent = clickEvent;
        }
    }

    public class TextCell : ICellType
    {
        public string Text { get; set; }
        public TextCell(string text)
        {
            Text = text;
        }
    }
}
