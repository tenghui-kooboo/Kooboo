formmeta={
  sumbitUrl:"/user/post",
  method:"POST",
  loaddataUrl:"",
  layout: "horizontal",
  items: [
    {
      type: "textbox",
      label: "textbox",
      name: "tb",
      value: "init value",
      placeholder: "input here",
      class: "input-small",
      tooltip: ""
    },
    {
      type: "textarea",
      label: "textarea",
      name: "path",
      value: "",
      placeholder: "input here",
      class: "",
      tooltip: "Textarea tooltip"
    },
    {
      type: "checkbox",
      label: "checkbox",
      name: "path",
      value: '["a"]',
      placeholder: "input here",
      tooltip: "",
      options: [
        {
          displayName: "A",
          value: "a"
        },
        {
          displayName: "B",
          value: "b"
        },
        {
          displayName: "C",
          value: "c"
        }
      ]
    },
    {
      type: "number",
      label: "number",
      name: "path",
      value: "",
      placeholder: "input here",
      class: "input-small",
      tooltip: ""
    },
    {
      type: "radiobox",
      label: "radiobox",
      name: "path",
      value: "b",
      placeholder: "input here",
      tooltip: "hello Radio tooltip",
      options: [
        {
          displayName: "A",
          value: "a"
        },
        {
          displayName: "B",
          value: "b"
        },
        {
          displayName: "C",
          value: "c"
        }
      ]
    },
    {
      type: "selection",
      label: "selection",
      name: "path",
      value: "a",
      placeholder: "input here",
      class: "input-small",
      tooltip: "",
      options: [
        {
          displayName: "A",
          value: "a"
        },
        {
          displayName: "B",
          value: "b"
        },
        {
          displayName: "C",
          value: "c"
        }
      ]
    },
    {
      type: "datetime",
      label: "datetime",
      name: "path",
      value: "",
      placeholder: "input here",
      class: "input-medium",
      tooltip: ""
    }
  ]
}