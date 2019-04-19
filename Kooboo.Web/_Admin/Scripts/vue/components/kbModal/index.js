(function () {
  Kooboo.vue.component.kbModal = Vue.component("kb-modal", {
    props: {
      config: {
        type: Object,
        default: {}
      },
      show: Boolean
    },
    data: function () {
      return {};
    },
    methods: {
      onclick(btn) {
        var self = this;
        switch (btn.typeStr) {
          case "submit":
            this.$parent.$refs.form.validate(function (err) {
              debugger;
              if (!err) {
                self.$parent.$refs.form.submit().then()
              }
            })
            break;

          default:
            break;
        }
      },
      onClose: function () {
        this.$emit("close");
      }
    },
    watch: {
      show: function (show) {
        $(this.$el).modal(show ? "show" : "hide");
      }
    },
    computed: {
      modalSize: function () {
        return this.config.size ? "modal-" + this.config.size : "";
      }
    },
    mounted: function () {
      var self = this;
      $(this.$el).on("show.bs.modal", function () {
        self.config.onShow && self.config.onShow();
      });
      $(this.$el).on("shown.bs.modal", function () {
        self.config.onShown && self.config.onShown();
      });
      $(this.$el).on("hide.bs.modal", function () {
        self.config.onHide && self.config.onHiden();
      });
      $(this.$el).on("hidden.bs.modal", function () {
        self.config.onHiden && self.config.onHiden();
      });
    },
    template: Kooboo.getTemplate(
      "/_Admin/Scripts/vue/components/kbModal/index.html"
    )
  });
})();