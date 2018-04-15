<template>
    <h1 class="page-title"> {{ title }}
        <small>{{ subtitle }}</small>
    </h1>
</template>
<script>
export default {
    data() {
        return {
            title: "Test",
            subtitle: "Subtitle"
        }
    },
    methods: {
        setTitle(param) {
            if ('title' in param)
                this.title = param.title

            if ('subtitle' in param)
                this.subtitle = param.subtitle
        }
    },
    mounted() {
        // Bus on
        this.$bus.$on('set-title', this.setTitle)
    },
    beforeDestroy() {
        // Bus off
        this.$bus.$off('set-title', this.setTitle)
    }
}
</script>
