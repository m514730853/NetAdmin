<template>
    <!-- 通栏布局 -->
    <template v-if="layout === 'header'">
        <header class="adminui-header">
            <div @click="logoClick" class="adminui-header-left">
                <div class="logo-bar">
                    <img class="logo logo-fill-color" src="@/assets/img/logo.svg" />
                    <div>
                        <p>{{ $CONFIG.APP_NAME }}</p>
                        <p class="version color-secondary">{{ version }}</p>
                    </div>
                </div>
                <ul v-if="!ismobile" class="nav">
                    <li v-for="item in menu" :class="pmenu.path === item.path ? 'active' : ''" :key="item" @click="showMenu(item)">
                        <el-icon>
                            <component :is="item.meta.icon || 'el-icon-menu'" />
                        </el-icon>
                        <span>{{ item.meta.title }}</span>
                    </li>
                </ul>
            </div>
            <div class="adminui-header-right">
                <userbar></userbar>
            </div>
        </header>
        <section class="aminui-wrapper">
            <div
                v-if="(!ismobile && nextMenu.length > 0) || (!pmenu.component && pmenu.meta)"
                :class="menuIsCollapse ? 'aminui-side isCollapse' : 'aminui-side'">
                <div v-if="!menuIsCollapse" class="adminui-side-top">
                    <h2>{{ pmenu.meta.title }}</h2>
                </div>
                <div class="adminui-side-scroll">
                    <el-scrollbar>
                        <el-menu :collapse="menuIsCollapse" :default-active="active" :unique-opened="$CONFIG.APP_SET_MENU_UNIQUE_OPENED" router>
                            <NavMenu :navMenus="nextMenu"></NavMenu>
                        </el-menu>
                    </el-scrollbar>
                </div>
                <div @click="$store.commit('TOGGLE_menuIsCollapse')" class="adminui-side-bottom">
                    <el-icon>
                        <el-icon-expand v-if="menuIsCollapse" />
                        <el-icon-fold v-else />
                    </el-icon>
                </div>
            </div>
            <Side-m v-if="ismobile"></Side-m>
            <div class="aminui-body el-container">
                <Tags v-if="!ismobile && layoutTags" :vue="this"></Tags>
                <div class="adminui-main" id="adminui-main">
                    <router-view v-slot="{ Component }" :key="routerViewKey">
                        <keep-alive :max="5">
                            <component v-if="$store.state.keepAlive.routeShow" :is="Component" :key="$route.fullPath" />
                        </keep-alive>
                    </router-view>
                    <iframe-view></iframe-view>
                </div>
            </div>
        </section>
    </template>

    <!-- 经典布局 -->
    <template v-else-if="layout === 'menu'">
        <header class="adminui-header">
            <div @click="logoClick" class="adminui-header-left">
                <div class="logo-bar">
                    <img class="logo logo-fill-color" src="@/assets/img/logo.svg" />
                    <div>
                        <p>{{ $CONFIG.APP_NAME }}</p>
                        <p class="version">{{ version }}</p>
                    </div>
                </div>
            </div>
            <div class="adminui-header-right">
                <userbar></userbar>
            </div>
        </header>
        <section class="aminui-wrapper">
            <div v-if="!ismobile" :class="menuIsCollapse ? 'aminui-side isCollapse' : 'aminui-side'">
                <div class="adminui-side-scroll">
                    <el-scrollbar>
                        <el-menu :collapse="menuIsCollapse" :default-active="active" :unique-opened="$CONFIG.APP_SET_MENU_UNIQUE_OPENED" router>
                            <NavMenu :navMenus="menu"></NavMenu>
                        </el-menu>
                    </el-scrollbar>
                </div>
                <div @click="$store.commit('TOGGLE_menuIsCollapse')" class="adminui-side-bottom">
                    <el-icon>
                        <el-icon-expand v-if="menuIsCollapse" />
                        <el-icon-fold v-else />
                    </el-icon>
                </div>
            </div>
            <Side-m v-if="ismobile"></Side-m>
            <div class="aminui-body el-container">
                <Tags v-if="!ismobile && layoutTags" :vue="this"></Tags>
                <div class="adminui-main" id="adminui-main">
                    <router-view v-slot="{ Component }" :key="routerViewKey">
                        <keep-alive :max="5">
                            <component v-if="$store.state.keepAlive.routeShow" :is="Component" :key="$route.fullPath" />
                        </keep-alive>
                    </router-view>
                    <iframe-view></iframe-view>
                </div>
            </div>
        </section>
    </template>

    <!-- 功能坞布局 -->
    <template v-else-if="layout === 'dock'">
        <header class="adminui-header">
            <div @click="logoClick" class="adminui-header-left">
                <div class="logo-bar">
                    <img class="logo logo-fill-color" src="@/assets/img/logo.svg" />
                    <div>
                        <p>{{ $CONFIG.APP_NAME }}</p>
                        <p class="version">{{ version }}</p>
                    </div>
                </div>
            </div>
            <div class="adminui-header-right">
                <div v-if="!ismobile" class="adminui-header-menu">
                    <el-menu
                        :default-active="active"
                        active-text-color="var(--el-color-primary)"
                        mode="horizontal"
                        router
                        text-color="var(--el-text-color-primary)">
                        <NavMenu :navMenus="menu"></NavMenu>
                    </el-menu>
                </div>
                <Side-m v-if="ismobile"></Side-m>
                <userbar></userbar>
            </div>
        </header>
        <section class="aminui-wrapper">
            <div class="aminui-body el-container">
                <Tags v-if="!ismobile && layoutTags" :vue="this"></Tags>
                <div class="adminui-main" id="adminui-main">
                    <router-view v-slot="{ Component }" :key="routerViewKey">
                        <keep-alive :max="5">
                            <component v-if="$store.state.keepAlive.routeShow" :is="Component" :key="$route.fullPath" />
                        </keep-alive>
                    </router-view>
                    <iframe-view></iframe-view>
                </div>
            </div>
        </section>
    </template>
    <!-- 默认布局 -->
    <template v-else>
        <section class="aminui-wrapper">
            <div v-if="!ismobile" class="aminui-side-split">
                <div class="aminui-side-split-top">
                    <router-link :to="$CONFIG.DASHBOARD_URL">
                        <img :title="$CONFIG.APP_NAME" class="logo logo-fill-color" src="@/assets/img/logo.svg" />
                    </router-link>
                </div>
                <div class="adminui-side-split-scroll">
                    <el-scrollbar>
                        <ul>
                            <li v-for="item in menu" :class="pmenu.path === item.path ? 'active' : ''" :key="item" @click="showMenu(item)">
                                <el-icon>
                                    <component :is="item.meta.icon || 'el-icon-menu'" />
                                </el-icon>
                                <p>{{ item.meta.title }}</p>
                            </li>
                        </ul>
                    </el-scrollbar>
                </div>
            </div>
            <div
                v-if="(!ismobile && nextMenu.length > 0) || (!pmenu.component && pmenu.meta)"
                :class="menuIsCollapse ? 'aminui-side isCollapse' : 'aminui-side'">
                <div v-if="!menuIsCollapse" class="adminui-side-top">
                    <h2>{{ pmenu.meta.title }}</h2>
                </div>
                <div class="adminui-side-scroll">
                    <el-scrollbar>
                        <el-menu :collapse="menuIsCollapse" :default-active="active" :unique-opened="$CONFIG.APP_SET_MENU_UNIQUE_OPENED" router>
                            <NavMenu :navMenus="nextMenu"></NavMenu>
                        </el-menu>
                    </el-scrollbar>
                </div>
                <div @click="$store.commit('TOGGLE_menuIsCollapse')" class="adminui-side-bottom">
                    <el-icon>
                        <el-icon-expand v-if="menuIsCollapse" />
                        <el-icon-fold v-else />
                    </el-icon>
                </div>
            </div>
            <Side-m v-if="ismobile"></Side-m>
            <div class="aminui-body el-container">
                <Topbar>
                    <userbar></userbar>
                </Topbar>
                <Tags v-if="!ismobile && layoutTags" :vue="this"></Tags>
                <div class="adminui-main" id="adminui-main">
                    <router-view v-slot="{ Component }" :key="routerViewKey">
                        <keep-alive :max="5">
                            <component v-if="$store.state.keepAlive.routeShow" :is="Component" :key="$route.fullPath" />
                        </keep-alive>
                    </router-view>
                    <iframe-view></iframe-view>
                </div>
            </div>
        </section>
    </template>

    <div @click="exitMaximize" class="main-maximize-exit">
        <el-icon>
            <el-icon-close />
        </el-icon>
    </div>

    <auto-exit></auto-exit>
</template>

<script>
import SideM from './components/sideM.vue'
import Topbar from './components/topbar.vue'
import Tags from './components/tags.vue'
import NavMenu from './components/NavMenu.vue'
import userbar from './components/userbar.vue'
import iframeView from './components/iframeView.vue'
import autoExit from './other/autoExit.js'

export default {
    name: 'index',
    components: {
        SideM,
        Topbar,
        Tags,
        NavMenu,
        userbar,
        iframeView,
        autoExit,
    },
    data() {
        return {
            routerViewKey: 0,
            menu: [],
            nextMenu: [],
            pmenu: {},
            active: '',
            version: '',
        }
    },
    computed: {
        ismobile() {
            return this.$store.state.global.ismobile
        },
        layout() {
            return this.$store.state.global.layout
        },
        layoutTags() {
            return this.$store.state.global.layoutTags
        },
        menuIsCollapse() {
            return this.$store.state.global.menuIsCollapse
        },
    },
    async created() {
        this.onLayoutResize()
        window.addEventListener('resize', this.onLayoutResize)
        const menu = this.$router.sc_getMenu()
        this.menu = this.filterUrl(menu)
        this.showThis()
        const res = await this.$API.sys_tools.getVersion.post()
        this.version = res.data.slice(0, res.data.indexOf('+'))
    },
    watch: {
        $route() {
            this.showThis()
        },
        layout: {
            handler(val) {
                document.body.setAttribute('data-layout', val)
            },
            immediate: true,
        },
    },
    methods: {
        logoClick() {
            this.$router.push({ path: '/' })
        },
        onLayoutResize() {
            this.$store.commit('SET_ismobile', document.body.clientWidth < 992)
        },
        //路由监听高亮
        showThis() {
            this.pmenu = this.$route.meta.breadcrumb ? this.$route.meta.breadcrumb[0] : {}
            this.nextMenu = this.filterUrl(this.pmenu.children)
            this.$nextTick(() => {
                this.active = this.$route.meta.active || this.$route.fullPath
            })
        },
        //点击显示
        showMenu(route) {
            this.pmenu = route
            this.nextMenu = this.filterUrl(route.children)
            if ((!route.children || route.children.length === 0) && route.component) {
                this.$router.push({ path: route.path })
            }
        },
        //转换外部链接的路由
        filterUrl(map) {
            const newMap = []
            map &&
                map.forEach((item) => {
                    item.meta = item.meta ? item.meta : {}
                    //处理隐藏
                    if (item.meta.hidden || item.meta.type === 'button') {
                        return false
                    }
                    //处理http
                    if (item.meta.type === 'iframe') {
                        item.path = `/i/${item.name}`
                    }
                    //递归循环
                    if (item.children && item.children.length > 0) {
                        item.children = this.filterUrl(item.children)
                    }
                    newMap.push(item)
                })
            return newMap
        },
        //退出最大化
        exitMaximize() {
            document.getElementById('app').classList.remove('main-maximize')
        },
    },
}
</script>
<style scoped>
.version {
    font-size: var(--el-font-size-small);
    font-weight: var(--el-font-weight-primary);
}
</style>