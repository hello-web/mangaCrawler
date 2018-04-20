<template>
    <div class="row">
        <div class="col-md-4">
            <div class="portlet light">
                <div class="portlet-title">
                    <div class="caption font-green-sharp">
                        <i class="icon-speech font-green-sharp"></i>
                        <span class="caption-subject">Manga Info</span>
                        <span class="caption-helper"></span>
                    </div>
                    <div class="actions"></div>
                </div>
                <div class="portlet-body">
                    <a class="thumbnail">
                        <img :src="thumbUrl">
                    </a>
                    <p>Title : {{ manga.Title }}</p>
                    <p>Chapter Total : {{ chapterList.length }}</p>
                    <button class="btn red" @click="back"><i class="icon-action-undo"></i> BACK</button>
                </div>
            </div>
        </div>
        <div class="col-md-8">
            <div class="portlet light">
                <div class="portlet-title">
                    <div class="caption font-green-sharp">
                        <i class="icon-speech font-green-sharp"></i>
                        <span class="caption-subject">Chapter List</span>
                        <span class="caption-helper"></span>
                    </div>
                    <div class="actions"></div>
                </div>
                <div class="portlet-body">
                    <div class="mt-element-card mt-element-overlay">
                        <div class="row flex-container">
                            <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12 flex-item" v-for="(item,x) in chapterList" :key="x">
                                <div class="mt-card-item">
                                    <div class="mt-card-avatar mt-overlay-4 none">
                                        <div class="img-container img-manga"></div>
                                        <div class="mt-overlay">
                                            <h2>{{ item.Title }}</h2>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<style>
.mt-card-avatar.none .mt-overlay {
    background-color: transparent;
}

.mt-card-avatar.none .mt-overlay,
.mt-card-avatar.none h2 {
    opacity: 1;
}

.mt-card-avatar.none h2 {
    transform: translatey(0);
}
</style>
<script>
export default {
    data() {
        return {}
    },
    computed: {
        chapterList() {
            return this.$store.getters['chapter/chapterlist']
        },
        manga() {
            return this.$store.getters['manga/mangacurrent']
        },
        thumbUrl() {
            if (this.manga.Thumb == null || this.manga.Thumb == "")
                return this.manga.ThumbUrl
            else
                return 'http://local.com/' + this.manga.Thumb
        }
    },
    methods: {
        refreshChapter(manga) {
            if (typeof manga != 'object')
                this.$router.push({name: 'home'})
            
            this.$store.dispatch('manga/setManga', manga)
            this.$store.dispatch('chapter/refreshChapter')
        },
        back() {
            this.$router.go(-1)
        }
    },
    mounted() {
        this.refreshChapter(this.$route.params.manga)
    }
}
</script>
