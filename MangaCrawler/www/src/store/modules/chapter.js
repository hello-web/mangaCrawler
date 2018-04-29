export default {
    namespaced: true,
    state: {
        chapterCurrent: null,
        chapterList: [],
        chapterTotal: 0,
        page: 1,
        maxPage: 0,
    },
    mutations: {
        clearChapter(state) {
            state.chapterList = []
        },
        pushChapter(state, chapter) {
            state.chapterList.push(chapter)
        },
        setPage(state, page) {
            state.page = page
        },
        setCurrentChapter(state, chapter) {
            state.chapterCurrent = chapter
        },
        setMaxPage(state, page) {
            state.maxPage = page
        },
        setChapterTotal(state, total) {
            state.chapterTotal = total
        }
    },
    getters: {
        chapterlist(state) {
            return state.chapterList
        },
        chaptercurrent(state) {
            return state.chapterCurrent
        },
        chapterId(state) {
            if (state.chapterCurrent != null)
                return state.chapterCurrent.Id
            return null
        }
    },
    actions: {
        refreshChapter(context, is_update) {
            context.commit('clearChapter')
            context.commit('setChapterTotal', 0)

            let currentProvider = context.rootGetters['provider/providerId']
            let currentManga = context.rootGetters['manga/mangaId']
            let currentPage = context.state.page
            
            // Get from database
            CS.getChapterList(currentProvider, currentManga, currentPage, is_update, x => {
                if (x == '')
                    return

                let data = JSON.parse(x)
                let items = data.data
                items.forEach(x => context.commit('pushChapter', x))
                context.commit('setMaxPage', data.maxPage)
                context.commit('setChapterTotal', data.total)
            })
        },
        nextPage(context) {
            let page = context.state.page + 1
            context.dispatch('goPage', page)
        },
        prevPage(context) {
            let page = context.state.page - 1
            context.dispatch('goPage', page)
        },
        resetPage(context) {
            context.commit('setPage', 1)
        },
        goPage(context, page) {
            let maxPage = context.state.maxPage
            let curPage = context.state.page
            if (page < 1 || page > maxPage || page == curPage)
                return              //ignore action
            
            context.commit('setPage', page)
            context.dispatch('refreshChapter', false)
        }
    }
}