<template>
  <kb-modal :show="showModal" :config="{title:'hello',
      showCloseBtn:true,
      closeBtnText:'取消',
      btns:[{
      class:'btn-primary',
      text:'保存',
      onClick:function(){}
    }]}" @close='showModal = false'>
  <div slot="body">
    <kb-form :meta="formData" ref="form"></kb-form>
  </div>
</kb-modal>

</template>

<script>
var components={
  data(){
    return {
      showModal: false,
      
    }
  }
}
</script>
