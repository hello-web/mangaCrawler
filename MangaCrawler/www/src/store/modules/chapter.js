export default {
    namespaced: true,
    state: {
        activeManga: null,
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
        changeActiveManga(state, manga) {
            state.activeManga = manga
        }
    },
    getters: {
        //
    },
    actions: {
        refreshChapter(context, manga) {
            context.commit('clearChapter')
            context.commit('changeActiveManga', manga)

            let currentPage = context.state.page
            let currentManga = context.state.activeManga.id

            // Get from database
            CS.getChapterList(currentManga, currentPage, x => {
                if (x == '')
                    return

                let data = JSON.parse(x)
            })
        }
    }
}