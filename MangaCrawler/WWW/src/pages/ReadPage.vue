<template>
    <div>
        <button class="btn red" @click="back"><i class="icon-action-undo"></i> BACK</button>
        <img v-for="(p, idx) in pagelist" :key="idx" :src="p.Url">
    </div>
</template>
<script>
export default {
    data() {
        return {}
    },
    computed: {
        pagelist() {
            return this.$store.getters['page/pagelist']
        }
    },
    methods: {
        back() {
            this.$router.go(-1)
        },
        refreshPage(chapter) {
            if (typeof chapter != 'object')
                this.$router.push({name: 'home'})
            
            this.$store.commit('chapter/setCurrentChapter', chapter)
            this.$store.dispatch('page/refreshPage')
        }
    },
    mounted() {
        this.refreshPage(this.$route.params.chapter)
    }
}
</script>
