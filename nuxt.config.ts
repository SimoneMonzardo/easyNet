import { NodeGlobalsPolyfillPlugin } from '@esbuild-plugins/node-globals-polyfill';

export default defineNuxtConfig({
  app: {
    head: {
      link: [{ 
        rel: 'icon', 
        type: 'image/x-icon', 
        href: '/muznet-white.ico' 
      }],
        htmlAttrs: {
            lang: 'it',
        },
      }
    },
  css: ["~/assets/css/main.css"],
  postcss: {
    plugins: {
      tailwindcss: {},
      autoprefixer: {},
    },
  },
  typescript: {
    strict: true,
  },
  modules: [
    "@nuxtjs/tailwindcss", 
    ["@nuxtjs/google-fonts", {
      families: {
        'Roboto': true,
        download: true,
        inject: true
      }
    }]
  ],
  vite: {
    optimizeDeps: {
      esbuildOptions: {
        define: {
          global: 'globalThis'
        },
        plugins: [
          NodeGlobalsPolyfillPlugin({
            buffer: true
          })
        ]
      }
    }
  },
});
