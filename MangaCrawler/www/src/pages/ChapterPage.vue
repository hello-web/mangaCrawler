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
                    <p>Chapter Total : {{ chapterTotal }}</p>
                    <button class="btn red" @click="back"><i class="icon-action-undo"></i> BACK</button>
                    <button class="btn red" @click="back"><i class="icon-refresh"></i> UPDATE</button>
                    <button class="btn red" @click="back"><i class="icon-drawer"></i> DOWNLOAD</button>
                    <button class="btn red" @click="back"><i class="icon-badge"></i> BOOKMARK</button>
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
                                            <div class="mt-info">
                                                <div class="mt-card-content">
                                                    <div class="mt-card-social">
                                                        <ul>
                                                            <li>
                                                                <a @click="readChapter(item)">
                                                                    <i class="icon-book-open"></i>
                                                                </a>
                                                            </li>
                                                            <li>
                                                                <a>
                                                                    <i class="icon-drawer"></i>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <ul class="pagination">
                                <li>
                                    <a @click="prevPage">
                                        <i class="fa fa-angle-left"></i>
                                    </a>
                                </li>
                                <li v-for="i in maxPage" :key="i" :class="{'active': i == currentPage }">
                                    <a @click="gotoPage(i)">{{ i }}</a>
                                </li>
                                <li>
                                    <a @click="nextPage">
                                        <i class="fa fa-angle-right"></i>
                                    </a>
                                </li>
                            </ul>
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
.mt-card-avatar.none h2,
.mt-card-avatar.none .mt-info {
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
        chapterTotal() {
            return this.$store.state.chapter.chapterTotal
        },
        manga() {
            return this.$store.getters['manga/mangacurrent']
        },
        thumbUrl() {
            if (this.manga.Thumb == null || this.manga.Thumb == "")
                return this.manga.ThumbUrl
            else
                return 'http://local.com/' + this.manga.Thumb
        },
        maxPage() {
            return this.$store.state.chapter.maxPage
        },
        currentPage() {
            return this.$store.state.chapter.page
        }
    },
    methods: {
        readChapter(chapter) {
            this.$router.push({name: 'read', params: { chapter }})
        },
        refreshChapter(manga, is_update) {
            if (typeof manga != 'object') {
                if (this.manga == null)
                    this.$router.push({name: 'home'})
                else
                    return;
            }
            
            this.$store.commit('manga/setCurrentManga', manga)
            this.$store.dispatch('chapter/resetPage')
            this.$store.dispatch('chapter/refreshChapter', is_update)
        },
        back() {
            this.$router.go(-1)
        },
        prevPage() {
            this.$store.dispatch('chapter/prevPage')
        },
        nextPage() {
            this.$store.dispatch('chapter/nextPage')
        },
        gotoPage(page) {
            this.$store.dispatch('chapter/goPage', page)
        }
    },
    mounted() {
        this.refreshChapter(this.$route.params.manga, false)
    }
}
</script>
