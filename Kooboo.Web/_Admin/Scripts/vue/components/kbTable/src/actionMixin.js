export default {
  computed: {
    actionType() {
      if (this.meta.action && this.action instanceof Object) {
        var actions = ["redirect", "newWindow", "popup", "post", "event"];
        if (this.meta.action.type && actions.length > this.meta.action.type) {
          return actions[this.meta.action.type];
        }
      }
      return this.meta.action
    },
    action() {
      //new action format：{type:0,confirm:"",uri}
      if (this.meta.action && this.meta.action instanceof Object) {
        return this.meta.action;
      }
      //old action format

      return this.meta;
    },
    isAction() {
      return ["post", "event", "popup"].indexOf(this.actionType) > -1;
    },
    actionMeta() {
      //define meta in action 
      if (this.meta && this.meta.action) {
        return this.meta.action.meta;
      }
      return null;
    }
  }
}