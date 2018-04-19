export default {
    namespaced: true,
    state: {
        chapterList: [],
        page: 1,
        maxPage: 0,
    },
    mutations: {
        clearChapter(state) {
            state.chapterList = []
            state.page = 1
        },
        pushChapter(state, chapter) {
            state.chapterList.push(chapter)
        }
    },
    getters: {
        chapterlist(state) {
            return state.chapterList
        }
    },
    actions: {
        refreshChapter(context) {
            context.commit('clearChapter')

            let currentProvider = context.rootGetters['provider/providerId']
            let currentManga = context.rootGetters['manga/mangaId']
            let currentPage = context.state.page
            
            // Get from database
            CS.getChapterList(currentProvider, currentManga, currentPage, true, x => {
                if (x == '')
                    return

                let data = JSON.parse(x)
                data.forEach(x => context.commit('pushChapter', x))
            })
        }
    }
}