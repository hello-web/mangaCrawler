export default {
    namespaced: true,
    state: {
        pageCurrent: null,
        pageList: []
    },
    mutations: {
        clearPage(state) {
            state.pageList = []
        },
        pushPage(state, page) {
            state.pageList.push(page)
        },
        setCurrentPage(state, page) {
            state.pageCurrent = page
        }
    },
    getters: {
        pagelist(state) {
            return state.pageList
        }
    },
    actions: {
        refreshPage(context) {
            context.commit('clearPage')

            let currentProvider = context.rootGetters['provider/providerId']
            let currentManga = context.rootGetters['manga/mangaId']
            let currentChapter = context.rootGetters['chapter/chapterId']
            
            // Get from database
            CS.getPageList(currentProvider, currentManga, currentChapter, true, x => {
                if (x == '')
                    return
                
                let data = JSON.parse(x)
                data.forEach(x => context.commit('pushPage', x))
            })
        }
    }
}