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
                        <a class="btn btn-circle red-sunglo "><i class="fa fa-plus"></i> Add</a>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="mt-element-card mt-element-overlay">
                        <div class="row">
                            <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12" v-for="i in 20" :key="i">
                                <div class="mt-card-item">
                                    <div class="mt-card-avatar mt-overlay-4">
                                        <img src="https://myanimelist.cdn-dena.com/images/manga/2/196863.jpg">
                                        <div class="mt-overlay">
                                            <h2>Soul Land 3:The Legend of the Dragon King Chapter 7</h2>
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
                        <div class="col-md-12">
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
                    <router-link to="/product" class="back-link">Product</router-link>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    data() {
        return {}
    }
}
</script>
