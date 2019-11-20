<template>
  <q-page class="flex flex-center">
    <q-slide-transition appear>
      <q-card class="semi-transparent" transition-show="slide-down" transition-hide="slide-up">
        <q-card-section class="bg-primary text-white semi-transparent">
          <div class="text-h6"><q-icon name="perm_identity" size="md" left/>Login first</div>
        </q-card-section>

        <q-separator />
        <q-card-section>
          <q-form>
            <q-input color="primary" v-model="form.username" label="Username" clearable clear-icon="close">
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
          </q-form>
        </q-card-section>

        <q-card-actions align="right">
          <q-btn class="bg-primary text-white full-width semi-transparent" @click="doLogin" :loading="isLoading">Login</q-btn>
        </q-card-actions>
      </q-card>
    </q-slide-transition>
  </q-page>
</template>

<script>
export default {
  name: 'LoginPage',
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
  methods: {
    doLogin: function () {
      this.isLoading = true
      const authModel = { username: this.form.username, password: this.form.password }
      this.$axios.post('http://localhost:51644/api/users/auth',
        {
          ...authModel
        })
        .then((response) => {
          this.isLoading = false
          console.log(response)
        })
        .catch((error) => {
          this.isLoading = false
          console.error(error)
        })
    }
  }
}
</script>
