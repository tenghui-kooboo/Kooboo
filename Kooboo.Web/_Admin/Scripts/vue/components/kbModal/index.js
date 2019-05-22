(function() {
  Kooboo.vue.component.kbModal = Vue.component("kb-modal", {
    props: {
      config: {
        type: Object,
        default: {}
      },
      show: Boolean
    },
    data: function() {
      return {};
    },
    methods: {
      onClose: function() {
        this.$refs.body.children[0].__vue__.reset();
        this.$emit("close");
      },
      onClick: function(type) {
        switch (type) {
          case "submit":
            var self = this,
              form = this.$refs.body.children[0].__vue__;

            form.validate(function(err) {
              if (!err) {
                form
                  .submit()
                  .then(function(res) {
                    self.onClose();
                  })
                  .catch(function(ex) {
                    debugger;
                  });
              }
            });
            break;
          case "close":
            this.onClose();
            break;
        }
      }
    },
    watch: {
      show: function(show) {
        $(this.$el).modal(show ? "show" : "hide");
      }
    },
    computed: {
      modalSize: function() {
        return this.config.size ? "modal-" + this.config.size : "";
      }
    },
    mounted: function() {
      var self = this;
      $(this.$el).on("show.bs.modal", function() {
        self.config.onShow && self.config.onShow();
      });
      $(this.$el).on("shown.bs.modal", function() {
        self.config.onShown && self.config.onShown();
      });
      $(this.$el).on("hide.bs.modal", function() {
        self.config.onHide && self.config.onHiden();
      });
      $(this.$el).on("hidden.bs.modal", function() {
        self.config.onHiden && self.config.onHiden();
      });
    },
    template: Kooboo.getTemplate(
      "/_Admin/Scripts/vue/components/kbModal/index.html"
    )
  });
})();
