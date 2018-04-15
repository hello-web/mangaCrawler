<template>
    <div class="row">
        <div class="col-md-12">
            <div class="portlet light">
                <div class="portlet-title">
                    <div class="caption font-green-sharp">
                        <i class="icon-speech font-green-sharp"></i>
                        <span class="caption-subject">Manga List</span>
                        <span class="caption-helper"></span>
                    </div>
                    <div class="actions">
                        <a class="btn btn-circle red-sunglo " @click="addCount"><i class="fa fa-plus"></i> Add</a>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="mt-element-card mt-element-overlay">
                        <div class="row flex-container">
                            <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12 flex-item" v-for="(data,i) in mangaList" :key="i">
                                <div class="mt-card-item">
                                    <div class="mt-card-avatar mt-overlay-4">
                                        <img :src="getThumbUrl(data)">
                                        <div class="mt-overlay">
                                            <h2>{{ data.Title }}</h2>
                                            <div class="mt-info font-white">
                                                <div class="mt-card-content">
                                                    <p class="mt-card-desc font-white">Description about manga</p>
                                                    <div class="mt-card-social">
                                                        <ul>
                                                            <li>
                                                                <a class="mt-card-btn">
                                                                    <i class="icon-social-facebook"></i>
                                                                </a>
                                                            </li>
                                                            <li>
                                                                <a class="mt-card-btn">
                                                                    <i class="icon-social-twitter"></i>
                                                                </a>
                                                            </li>
                                                            <li>
                                                                <a class="mt-card-btn">
                                                                    <i class="icon-social-dribbble"></i>
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
                            <ul class="pagination pagination-block">
                                <li>
                                    <a href="javascript:;">
                                        <i class="fa fa-angle-left"></i>
                                    </a>
                                </li>
                                <li>
                                    <a href="javascript:;"> 1 </a>
                                </li>
                                <li>
                                    <a href="javascript:;"> 2 </a>
                                </li>
                                <li>
                                    <a href="javascript:;"> 3 </a>
                                </li>
                                <li>
                                    <a href="javascript:;">
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
<script>
export default {
    data() {
        return {}
    },
    computed: {
        mangaList() {
            return this.$store.getters['manga/mangalist']
        }
    },
    methods: {
        getThumbUrl(item) {
            if (item.Thumb == null || item.Thumb == "")
                return item.ThumbUrl
            else
                return 'http://local.com/' + item.Thumb
        },
        addCount() {
            this.$router.push('/product')
            //this.$store.commit('manga/increment')
        }
    }
}
</script>
<style>
.flex-container {
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
}
.flex-container .flex-item {
    display: flex;
    flex-grow: 1;
    flex-shrink: 0;
    flex-basis: auto;
}
</style>