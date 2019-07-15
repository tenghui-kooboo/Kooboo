<template>
  <div class="modal fade" data-backdrop="static" data-keyboard="false">
    <div class="modal-dialog" :class="modalSize">
      <div class="modal-content">
        <div class="modal-header">
          <button class="close" @click="onClose">
            <i class="fa fa-close"></i>
          </button>
          <h4 class="modal-title">{{ config.title }}</h4>
        </div>
        <div class="modal-body" ref="body">
          <slot name="body"></slot>
        </div>
        <div class="modal-footer">
          <template v-for="(btn, idx) in config.buttons">
            <button :key="idx" class="btn" :class="btn.class" @click="onClick(btn.type)">{{ btn.text }}</button>
          </template>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  props: {
    config: {
      type: Object,
      default: {}
    },
    show: Boolean
  },
  inject: ['popup'],
  data: function () {
    return {};
  },
  methods: {
    onClose: function () {
      var form = this.popup.getPopupBody();
      if (form && form.reset) {
        form.reset();
      }
      //this.$refs.body.children[0].__vue__.reset();
      this.$emit("close");
    },
    onClick: function (type) {
      switch (type) {
        case "submit":
        case 0:
          var self = this;
          var form = this.popup.getPopupBody();
          if (form) {
            if (form && form.validate) {
              form.validate(function (err) {
                if (!err) {
                  form.submit()
                    .then(function (res) {
                      self.onClose();
                    }).catch(function (ex) {
                    });
                }
              });
            }
          }

          break;
        case "close":
        case 1:
          this.onClose();
          break;
      }
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
}
</script>
