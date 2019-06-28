using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Model.Meta.Tab;
using Kooboo.Sites.Models;
using Kooboo.Model.Meta;
using Kooboo.Model.Meta.Table;
using Kooboo.Web.ViewModel;
using Kooboo.Web.Api.Implementation;


namespace Kooboo.Web.Meta
{
    public class CodeMeta : ITabMetaConfigure<Code>
    {
        public void Configure(TabMeta meta)
        {
            var api = UrlHelper.ApiUrl<CodeApi>(nameof(CodeApi.ListByType));
            #region tab

            var menus = new List<Model.Meta.Table.MenuItem>();

            meta.Tabs.Add(new Tab
            {
                Text = new Localizable("All"),
                DataApi = api,
                Key ="codetype",
                Value="all",
                View=CreateTableMeta("All")
            });

            meta.Tabs.Add(new Tab
            {
                Text = new Localizable("Api"),
                DataApi = api,
                Key = "codetype",
                Value = "api",
                View = CreateTableMeta("Api")

            });

            meta.Tabs.Add(new Tab
            {
                Text = new Localizable("DataSource"),
                DataApi = api,
                Key = "codetype",
                Value = "dataSource",
                View= CreateTableMeta("DataSource")
            });

            meta.Tabs.Add(new Tab
            {
                Text = new Localizable("Diagnosis"),
                DataApi = api,
                Key = "codetype",
                Value = "diagnosis",
                View = CreateTableMeta("Diagnosis")
            });

            meta.Tabs.Add(new Tab
            {
                Text = new Localizable("Event"),
                DataApi = api,
                Key = "codetype",
                Value = "event",
                View = CreateTableMeta("Event")
            });

            meta.Tabs.Add(new Tab
            {
                Text = new Localizable("Job"),
                DataApi = api,
                Key = "codetype",
                Value = "job",
                View = CreateTableMeta("Job")
            });

            meta.Tabs.Add(new Tab
            {
                Text = new Localizable("PageScript"),
                DataApi = api,
                Key = "codetype",
                Value = "pagescript",
                View = CreateTableMeta("PageScript")
            });
            #endregion
        }

        #region

        public TableMeta CreateTableMeta(string type)
        {
            var meta = new TableMeta();
            CreateTableMenu(meta, type);

            meta.Builder<CodeListItem>()
                .Column<TextCell>(c => c.Name, new Localizable("Name"))
                .Column<LabelCell>(c=>c.CodeType,new Localizable("Code type"))
                .Column<LabelCell>(c => c.EventType, new Localizable("Type"))//isEmbedded,eventType-->complex
                .Column<ArrayCell>(c => c.References, new Localizable("References"))
                .Column<LinkCell>(c => c.PreviewUrl, new Localizable("Preview"))
                .Column<DateCell>(c => c.LastModified, new Localizable("Last modified"))
                ;

            return meta;
        }

        private void CreateTableMenu(TableMeta meta,string type)
        {
            var url = "/_Admin/Development/EditCode?codeType=" + type;

            switch (type.ToLower())
            {
                case "all":
                    meta.Menu.Add(new DropDownMenu
                    {
                        Text = new Localizable("Create"),
                        Class = "green",
                        Options=SelectOptions.UseList()
                    });

                    meta.Menu.Add(new ButtonMenu
                    {
                        Text = new Localizable("API Generator"),
                        Class = "green",
                        //Action
                    });
                    break;
                case "api":
                    meta.Menu.Add(new ButtonMenu
                    {
                        Text = new Localizable("Create Api"),
                        Class = "green",
                        Action = ActionMeta.Redirect(url)
                    });
                    meta.Menu.Add(new ButtonMenu
                    {
                        Text=new Localizable("API Generator"),
                        Class="green",
                        //Action
                    });
                    break;
                case "datasource":
                    meta.Menu.Add(new ButtonMenu
                    {
                        Text = new Localizable("Create DataSource"),
                        Class = "green",
                        Action = ActionMeta.Redirect(url)
                    });
                    break;
                case "diagnosis":
                    meta.Menu.Add(new ButtonMenu
                    {
                        Text = new Localizable("Create Diagnosis"),
                        Class = "green",
                        Action = ActionMeta.Redirect(url)
                    });
                    break;
                case "event":
                    meta.Menu.Add(new ButtonMenu
                    {
                        Text = new Localizable("Create Event"),
                        Class = "green",
                        Action = ActionMeta.Redirect(url)
                    });
                    break;
                case "job":
                    meta.Menu.Add(new ButtonMenu
                    {
                        Text = new Localizable("Create Job"),
                        Class = "green",
                        Action = ActionMeta.Redirect(url)
                    });
                    break;
                case "pagescript":
                    meta.Menu.Add(new ButtonMenu
                    {
                        Text = new Localizable("Create PageScript"),
                        Class = "green",
                        Action = ActionMeta.Redirect(url)
                    });
                    break;
            }
        }
        #endregion
    }
}
