<template>
  <q-page class="flex flex-center">
    <app-publiccard>
      <q-card-section class="bg-primary text-white semi-transparent">
        <div class="text-h6"><q-icon name="perm_identity" size="md" left/>Login first</div>
      </q-card-section>

      <q-separator />
      <q-card-section>
        <q-form @submit="doLogin">
          <q-input color="primary" type="text" v-model="form.username" label="Username" clearable clear-icon="close">
            <template v-slot:prepend>
              <q-icon name="perm_identity" />
            </template>
          </q-input>
          <br />
          <q-input color="primary" v-model="form.password" label="Password" :type="showPassword ? 'text' : 'password'" >
            <template v-slot:append>
              <q-icon
                :name="showPassword ? 'visibility_off' : 'visibility'"
                class="cursor-pointer"
                @click="showPassword = !showPassword"
              />
            </template>
          </q-input>
          <br />
          <q-btn class="bg-primary text-white full-width semi-transparent" @click="doLogin" :loading="isLoading" :disable="isLoading || !isFormValid">Login</q-btn>
        </q-form>
      </q-card-section>
    </app-publiccard>
  </q-page>
</template>

<script>
import UserService from 'services/UserService'
import PublicCard from 'components/layout/PublicCard'

export default {
  name: 'LoginPage',
  components: {
    'app-transition': AppTransition,
    'app-publiccard': PublicCard
  },
  data: function () {
    return {
      isLoading: false,
      showPassword: false,
      form: {
        username: '',
        password: ''
      }
    }
  },
  computed: {
    isFormValid: function () {
      return this.form.username != null && this.form.username.length !== 0 && this.form.password != null && this.form.password.length !== 0
    }
  },
  methods: {
    doLogin: function () {
      this.isLoading = true

      const userservice = new UserService()
      userservice.doAuthenticate(this.form.username, this.form.password).then((response) => {
        console.log(response)
        this.isLoading = false
        this.$q.notify('You are now logged in as <b>' + response.username + '</b>.')
      }).catch((response) => {
        console.error(response)
        this.isLoading = false
        this.$q.notify('Could not log you in with this credentials.')
      })
    }
  }
}
</script>
