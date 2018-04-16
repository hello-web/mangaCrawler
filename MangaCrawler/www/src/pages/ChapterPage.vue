<template>
    <div class="">
        Hi, this is product
        <router-link to="/" class="back-link">BACK</router-link>
    </div>
</template>
<script>
export default {
    data() {
        return {}
    },
    methods: {
        refreshChapter(id) {
            console.log(id)
        }
    },
    mounted() {
        this.refreshChapter(this.$route.params.id)
    }
}
</script>
