
export default {
  meta: {
    menu: [{
      type: "button",
      text: "New page",
      class: "green",
      action: "redirect",
      url: '/_Admin/Page/EditPage'
    }, {
      type: "button",
      text: "New rich text page",
      class: "green",
      action: "redirect",
      url: '/_Admin/Page/EditRichText'
    },{
      type: "dropdown",
      text: "New layout page",
      class: "green",
      action: 'redriect',
      url: '/_Admin/Page/EditLayout?layoutId={id}',
      options: {
        data: "layouts",
        text: "name"
      }
    }, {
      type: "button",
      text: "Copy",
      class: "green",
      action: "popup",
      url: 'page/copy?modelName=Layout',
      visible: {
        compare: '=',
        value: 1
      }
    },{
      type: "button",
      text: "Delete",
      class: "red",
      action: "post",
      confirm: 'confirm.deleteItemsWithRef',
      url: 'page/delete',
      visible: {
        compare: '>=',
        value: 1
      }
    },{
      type: "icon",
      align: "right",
      iconClass: "gear",
      action: "popup",
      url: '/_Admin/Page/Delete?ids={id}'
    }],
    columns: [{
      name: "name",
      header: {
        text: "Name",
      }
    },{
      name: "linked",
      header: {
        text: "Linked"
      },
      cell: {
        type: "badge"
      }
    },{
      name: "online",
      header: {
        text: "Online"
      },
      cell: {
        type: "label",
        class: [ { true: "green", false: "blue" } ],
        text: {
          true: "{online.yes}",
          false: "{online.no}"
        }
      }
    },{
      name: "relations",
      header: {
        text: "Relations",
      },
      cell: {
        type: "array",
        text:"{0} {key}",
      }
    },{
      name: "lastModified",
      header: {
        text: "Last modified",
      },
      cell: {
        type: 'date'
      }
    },{
      name: "previewUrl",
      header: {
        text: "Preview",
      },
      cell: {
        type: "link",
        action: "newWindow"
      }
    },{
      name: "setting",
      cell: {
        type: "button",
        class: "blue",
        innerText: "Setting",
        action: "redirect",
        url: "/page/details?id={id}&layoutId={layoutId}"
      }
    },{
      name: "inlineUrl",
      cell: {
        type: "button",
        class: "btn-primary hidden-xs",
        innerText: "Inline edit",
        action: "newWindow"
      }
    },{
      name: "qrCode",
      cell: {
        type: "icon",
        class: 'btn-xs',
        iconClass: "qrcode",
        action: "popup",
        url: "layout/arCode?modelName=Layout"
      }
    }]
  },
  data: {
    "baseUrl": "http://test.vango.kooboo.site/",
    "layouts": [
      {
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
      }
    ],
    "pages": [
      {
        "id": "f2b7bc62-2ff6-4ef7-b401-b5bdf86b6963",
        "name": "asdfsdfsdfdf",
        "title": null,
        "warning": 0,
        "linked": 0,
        "pageView": 1,
        "layoutId": "9a3e717f-e15b-a99e-9ebd-d69188b30ca0",
        "online": true,
        "lastModified": "2019-04-17T09:20:11.3808124Z",
        "path": "/asdfsdfsdfdf",
        "previewUrl": "http://test.vango.kooboo.site/asdfsdfsdfdf",
        "inlineUrl": "/_api/redirect/inline?siteid=05bff4d1-ea1b-28e7-fc81-000a0c2d02e7&pageid=f2b7bc62-2ff6-4ef7-b401-b5bdf86b6963",
        "startPage": false,
        "relations": {
          "view": 1,
          "layout": 1
        },
        "type": "Layout"
      },
      {
        "id": "c61674fb-042b-45eb-ae6a-e3753277d90c",
        "name": "test",
        "title": null,
        "warning": 0,
        "linked": 0,
        "pageView": 0,
        "layoutId": "00000000-0000-0000-0000-000000000000",
        "online": true,
        "lastModified": "2018-12-25T05:36:46.1282913Z",
        "path": "/test",
        "previewUrl": "http://test.vango.kooboo.site/test",
        "inlineUrl": "/_api/redirect/inline?siteid=05bff4d1-ea1b-28e7-fc81-000a0c2d02e7&pageid=c61674fb-042b-45eb-ae6a-e3753277d90c",
        "startPage": false,
        "relations": {},
        "type": "RichText"
      }
    ]
  }
}
