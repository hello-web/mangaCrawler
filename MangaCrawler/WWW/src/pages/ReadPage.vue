<template>
    <div>
        //
    </div>
</template>
<script>
export default {
    data() {
        return {}
    },
    methods: {
        refreshPage(chapter) {
            console.log(chapter)
        }
    },
    mounted() {
        this.refreshPage(this.$route.params.chapter)
    }
}
</script>
