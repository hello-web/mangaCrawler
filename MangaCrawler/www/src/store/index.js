import Vuex from 'vuex'
import Vue from 'vue'
import manga from './modules/manga'
import chapter from './modules/chapter'
import page from './modules/page'
import provider from './modules/provider'

Vue.use(Vuex)

export default new Vuex.Store({
    modules: {
        manga,
        chapter,
        page,
        provider,
    }
})
