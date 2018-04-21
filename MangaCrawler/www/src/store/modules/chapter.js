export default {
    namespaced: true,
    state: {
        chapterCurrent: null,
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
        },
        setCurrentChapter(state, chapter) {
            state.chapterCurrent = chapter
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