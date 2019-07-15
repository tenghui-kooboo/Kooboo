<template>
  <kb-modal :show="showModal" :config="metadata" @close="showModal = false">
    <template v-if="showModal">
      <template v-for="(view,index) in metadata.views">
        <Component
          :key="index"
          :is="`kb-${view.type}`"
          ref="body"
          slot="body"
          :ctx="ctx"
          :meta="view"
          :meta-name="view.metaName"
        ></Component>
      </template>
    </template>
  </kb-modal>
</template>
<script>
import kbModal from './modal'
import kbForm  from '../../kbForm/src/Index.vue'
export default {
  props: {
      visible: Boolean,
      meta: Object
    },
    provide(){
      return {
        popup:this
      }
    },
    data() {
      return {
        showModal: false,
        ctx: null,
        metadata:this.meta
      };
    },
    methods: {
      getPopupBody(){
        if(this.$refs.body && this.$refs.body.length > 0){
          return this.$refs.body[0]
        }
        return null
      },
      show(ctx) {
        this.ctx = ctx;
        var modelName=this.getModelName(ctx.parameters);
        if(modelName){
          //can't set value to prop directly
          this.metadata= api.popupMeta(modelName);
        }else if(this.ctx.meta){//get meta form context
          this.metadata=this.ctx.meta
        }

        this.showModal = true;
      },
      getModelName: function (parameters) {
        if (parameters["modelName"]) {
          return parameters["modelName"];
        }
        return "";
      },
    },
    
    watch: {
      visible(show) {
        if (show) {
          this.showModal = true;
        } else {
          this.showModal = false;
        }
      }
    },
    components: {
      kbModal,
      kbForm
    },
}
</script>

