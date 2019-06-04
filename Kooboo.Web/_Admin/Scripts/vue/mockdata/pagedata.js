//table data
var siteId = "2f3b128c-b919-e2c7-7379-667b7411d25b";
Mock.mock("/_api/page/all?SiteId=" + siteId, function () {
  return {
    model: {
      baseUrl: "http://asdf.kooboo/",
      layouts: [{
        "id": "9a3e717f-e15b-a99e-9ebd-d69188b30ca0",
        "name": "asdfasd",
        "extension": "html",
        "body": "<!DOCTYPE html>\n<html>\n\n<head>\n</head>\n\n<body>\n    <div k-placeholder=\"Main\"> Sample text inside the layout.. </div>\n</body>\n\n</html>",
        "online": true,
        "version": 3,
        "constType": 98,
        "creationDate": "2019-04-17T09:19:07.1776466Z",
        "lastModified": "2019-04-17T09:19:07.1776466Z",
        "lastModifyTick": 636910895471776466
      }],
      pages: [{
        id: "3104f1e5-7d0e-4d37-9124-c67c7ae10e4b",
        name: "aass",
        title: null,
        warning: 0,
        linked: 0,
        pageView: 0,
        layoutId: "00000000-0000-0000-0000-000000000000",
        online: true,
        lastModified: "2019-04-28T09:53:23.862451Z",
        path: "/aass",
        previewUrl: "http://asdf.kooboo/aass",
        inlineUrl: "/_api/redirect/inline?siteid=b88fea9f-2f3a-51a7-f38c-bf827c38918d&pageid=3104f1e5-7d0e-4d37-9124-c67c7ae10e4b",
        startPage: false,
        relations: {},
        type: "Normal"
      }]
    }

  }
});

//table meta
Mock.mock("/_api/meta/get?SiteId=" + siteId + "&modelname=page", function () {
  return {
    model: {
      listName: "page",
      dataApi: "/page/all",
      menu: [{
          type: "button",
          text: "New page",
          class: "green",
          action: "redirect",
          url: "/_Admin/Page/EditPage"
        },
        {
          type: "button",
          text: "New rich text page",
          class: "green",
          action: "redirect",
          url: "/_Admin/Page/EditRichText"
        },
        {
          type: "dropdown",
          text: "New layout page",
          class: "green",
          action: "redriect",
          url: "/_Admin/Page/EditLayout?layoutId={id}",
          options: {
            data: "layouts",
            text: "{name}"
          }
        },
        {
          type: "button",
          text: "Import",
          class: "green",
          action: "popup",
          url: "popup?modelName=importPopup"
        },
        {
          type: "button",
          text: "Copy",
          class: "green",
          action: "popup",
          url: "popup?modelName=copyPopup",
          visible: {
            compare: "=",
            value: 1
          }
        },
        {
          type: "button",
          text: "Delete",
          class: "red",
          action: "post",
          confirm: "confirm.deleteItemsWithRef",
          url: "page/delete",
          visible: {
            compare: ">=",
            value: 1
          }
        },
        {
          type: "icon",
          align: "right",
          iconClass: "gear",
          action: "popup",
          url: "popup?modelName=routeSettingPopup"
        }
      ],
      columns: [{
          name: "name",
          header: {
            text: "Name"
          }
        },
        {
          name: "linked",
          header: {
            text: "Linked"
          },
          cell: {
            type: "badge"
          }
        },
        {
          name: "online",
          header: {
            text: "Online"
          },
          cell: {
            type: "label",
            class: [{
              true: "green",
              false: "blue"
            }],
            text: {
              true: "{online.yes}",
              false: "{online.no}"
            }
          }
        },
        {
          name: "relations",
          header: {
            text: "Relations"
          },
          cell: {
            type: "array",
            text: "{0} {key}",
            action: "popup",
            url: "popup?modelName=relationPopup&id={id}&by={key}&type=page"
          }
        },
        {
          name: "lastModified",
          header: {
            text: "Last modified"
          },
          cell: {
            type: "date"
          }
        },
        {
          name: "previewUrl",
          header: {
            text: "Preview"
          },
          cell: {
            type: "link",
            action: "newWindow"
          }
        },
        {
          name: "setting",
          cell: {
            type: "button",
            class: "blue",
            innerText: "Setting",
            action: "redirect",
            url: "/page/details?id={id}&layoutId={layoutId}"
          }
        },
        {
          name: "inlineUrl",
          cell: {
            type: "button",
            class: "btn-primary hidden-xs",
            innerText: "Inline edit",
            action: "newWindow"
          }
        },
        {
          name: "qrCode",
          cell: {
            type: "icon",
            class: "btn-xs",
            iconClass: "qrcode",
            action: "event",
            url: "showQrcode"
          }
        }
      ]
    }

  }
});


//routeSettingPopup
Mock.mock("/_api/meta/get?SiteId=" + siteId + "&modelname=routeSettingPopup", function () {
  return {
    model: {
      title: "Route setting",
      description: {
        title: "Redirect routes",
        content: "Set the redirect pages for default home page, 404 and error pages",
        closable: true,
        class: "alert alert-info alert-dismissable"
      },
      buttons: [{
          class: "green",
          text: "save",
          type: "submit"
        },
        {
          class: "gray",
          text: "cancel",
          type: "close"
        }
      ],
      views: [{
        type: "form",
        metaName: "routeFormMeta"
      }]
    }

  }
});
// routeFormMeta
Mock.mock("/_api/meta/get?SiteId=" + siteId + "&modelname=routeFormMeta", function () {
  return {
    model: {
      loadApi: "/Page/DefaultRoute",
      submitApi: "/page/defaultRouteUpdate",
      items: [{
          type: "selection",
          label: "Home page",
          name: "startPage",
          placeholder: "",
          tooltip: "",
          options: {
            data: "context", //context:selected/list
            text: "{name}",
            value: "{id}"
          }
        },
        {
          type: "selection",
          label: "404 page",
          name: "notFound",
          placeholder: "",
          tooltip: "",
          options: {
            data: "context", //context:selected/list
            text: "{name}",
            value: "{id}",
            default: {
              text: "System default",
              value: "00000000-0000-0000-0000-000000000000"
            }
          }
        },
        {
          type: "selection",
          label: "Error page",
          name: "error",
          placeholder: "",
          tooltip: "",
          options: {
            data: "context", //context:selected/list
            text: "{name}",
            value: "{id}",
            default: {
              text: "System default",
              value: "00000000-0000-0000-0000-000000000000"
            }
          }
        }
      ]
    }

  }
});


//copyPopup 
Mock.mock("/_api/meta/get?SiteId=" + siteId + "&modelname=copyPopup", function () {
  return {
    model: {
      title: 'Copy', //copy view:aa-->copy {page.title}:{selectedrow.name}
      description: {
        title: '',
        content: '',
        closable: false
      },
      buttons: [{
        class: "green",
        text: "start",
        type: "submit",
      }, {
        class: "gray",
        text: "cancel",
        type: "close",
      }],
      views: [{
        type: 'form',
        metaName: "copyFormMeta",
      }]
    }

  }
});

//copy meta
Mock.mock("/_api/meta/get?SiteId=" + siteId + "&modelname=copyFormMeta", function () {
  return {
    model: {
      loadApi: "",
      submitApi: "/page/copy",
      layout: "horizontal",
      items: [{
        type: "textBox",
        label: "name",
        name: "name",
        placeholder: "",
        class: "col-md-9",
        tooltip: "",
        options: {
          data: "selected", //context:selected/list
          text: "{name}_copy"
        },
        rules: [{
            type: "required",
            message: 'required'
          },
          {
            type: "minLength",
            minLength: 1,
            message: 'minLength 1'
          },
          {
            type: "maxLength",
            maxLength: 64,
            message: 'maxLength 64'
          },
          {
            type: "unique",
            api: "/page/isUniqueName?name={name}",
            message: 'taken'
          }
        ]
      }, {
        type: "hidden",
        label: "id",
        name: "id",
        options: {
          data: "selected", //context:selected/list
          text: "{id}"
        }
      }, {
        type: "hidden",
        label: "url",
        name: "url",
        options: {
          data: "selected",
          text: "/{name}_copy"
        },
        rules: [{
            type: "required",
            message: 'required'
          },
          {
            type: "regex",
            //regex: "^[^\s|\~|\`|\!|\@|\#|\$|\%|\^|\&|\*|\(|\)|\+|\=|\||\[|\]|\;|\:|\"|\'|\,|\<|\>|\?]*$",
            regex:'.+',
            message: "invalid"
          }
        ]
      }]

    }
  }
});