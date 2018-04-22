<template>
    <div class="row">
        <div class="col-md-12">
            <div class="portlet light">
                <div class="portlet-title">
                    <div class="caption font-green-sharp">
                        <i class="icon-speech font-green-sharp"></i>
                        <span class="caption-subject">Manga Info</span>
                        <span class="caption-helper"></span>
                    </div>
                    <div class="actions"></div>
                </div>
                <div class="portlet-body row">
                    <div class="col-md-12">
                        <button class="btn red" @click="back"><i class="icon-action-undo"></i> BACK</button>
                        <button class="btn red" @click="prevPage"><i class="icon-action-undo"></i> Prev</button>
                        <button class="btn red" @click="nextPage"><i class="icon-action-undo"></i> Next</button>
                        <span>{{ currentIndex+1 }}/{{ pagecount }}</span>
                    </div>
                    <div class="col-md-12">
                        <img :src="pagesrc">
                    </div>
                    <div class="col-md-12">
                        <button class="btn red" @click="back"><i class="icon-action-undo"></i> BACK</button>
                        <button class="btn red" @click="prevPage"><i class="icon-action-undo"></i> Prev</button>
                        <button class="btn red" @click="nextPage"><i class="icon-action-undo"></i> Next</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    data() {
        return {
            currentIndex: 0
        }
    },
    computed: {
        pagecount() {
            return this.pagelist.length
        },
        pagesrc() {
            let page = this.pagelist[this.currentIndex]

            if (typeof page != 'undefined')
                return this.pagelist[this.currentIndex].Url
            return ''
        },
        pagelist() {
            return this.$store.getters['page/pagelist']
        }
    },
    methods: {
        back() {
            this.$router.go(-1)
        },
        nextPage() {
            if (this.currentIndex < this.pagecount - 1)
                this.currentIndex++;
        },
        prevPage() {
            if (this.currentIndex > 0)
                this.currentIndex--;
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
